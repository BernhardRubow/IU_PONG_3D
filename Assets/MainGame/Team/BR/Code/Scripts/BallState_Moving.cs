using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_Moving : MonoBehaviour
{
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [SerializeField] private float m_BallStartSpeed; 
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_AdditionalHitSpeed;
    [SerializeField] private Vector3 m_MoveDirection;
    [SerializeField] private AnimationCurve m_BallHitSpeedFactor;
    [SerializeField] private float m_AnimationTime;
    [SerializeField] private float m_AnimationTimeThreshold;
    [SerializeField] private Vector3 m_CurrentBallPosition;
    [SerializeField] private int m_HitTilBallDoubleSpeed = 20;


    /// <summary>
    /// The paddle hit are counted in the game after a player
    /// served. These determine the speed of the ball in the game.
    /// </summary>
    [SerializeField] private int m_PaddleHits = 0;

    /// <summary>
    /// This value controls the initial intensity of the acceleration of the Ball
    /// when it hits a paddle
    /// </summary>
    [SerializeField] private float m_AccelerationFactor = 0.01f;
    


    // +++ unity event handle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnEnable()
    {
        Debug.Log("BallState_Moving - OnEnable");
        MessageBus.Subscribe<Message_SideLineHit>(OnSideLineHit);
        MessageBus.Subscribe<Message_PaddleHit>(OnPaddleHit);

        // reset paddle hits, which are use to
        // raise the ball speed in the game
        m_PaddleHits = 0;
        
        m_CurrentBallPosition = transform.position;

        m_Speed = m_BallStartSpeed;

        m_MoveDirection = transform.position.x < 0
            ? Vector3.right
            : Vector3.left;
        m_MoveDirection.y = Random.Range(-1f, 1f);
        m_AnimationTime = 0f;

        m_AnimationTimeThreshold = Random.Range(2f, 3f);
    }

    void OnDisable()
    {
        Debug.Log("BallState_Moving - OnDisable");
        MessageBus.UnSubscribe<Message_SideLineHit>(OnSideLineHit);
        MessageBus.UnSubscribe<Message_PaddleHit>(OnPaddleHit);
    }

    void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        //Debug.Log(tag);

        switch (tag)
        {
            case "side-line":
                MessageBus.Publish(new Message_SideLineHit());
                break;

            case "paddle":
                m_PaddleHits++;
                MessageBus.Publish(new Message_PaddleHit());
                break;
        }
    }

    void Update()
    {
        CalculateAndApplyAccelerationAfterHit();

        MoveBall();

        CheckIfBallIsOutOfBounds();
    }


    // +++ Custom Eventhandler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnSideLineHit(object eventArgs)
    {
        m_MoveDirection.y *= -1f;
    }

    private void OnPaddleHit(object eventArgs)
    {
        m_AnimationTimeThreshold = Random.Range(2f, 3f);

        // store velocity of ball an normalize it as preparation
        // for set random vertical speed after paddle hit
        var moveDirectionMagnitude = m_MoveDirection.magnitude;
        m_MoveDirection = m_MoveDirection.normalized;
        
        // ball gets faster with every paddle hit
        var newSpeed = m_BallStartSpeed * ((m_PaddleHits / (float)m_HitTilBallDoubleSpeed) + 1f);
        Debug.Log($"NEW SPEED: {newSpeed}");
        m_Speed = newSpeed;

        // calcuate random direction after paddle hit
        m_MoveDirection.x *= -1f;
        m_MoveDirection.y = m_MoveDirection.y = Random.Range(-1f, 1f);
        m_MoveDirection += m_MoveDirection * moveDirectionMagnitude * m_AccelerationFactor;

        // the ball gets faster after a paddle hit
        // for a certain time
        m_AnimationTime = 0; ;
        m_AnimationTimeThreshold = Random.Range(2f, 3f);

        // count paddle hits
        m_PaddleHits++;
    }


    // +++ member +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    void CalculateAndApplyAccelerationAfterHit()
    {
        if (m_AnimationTime < m_AnimationTimeThreshold)
        {
            m_AnimationTime += Time.deltaTime;
            m_AdditionalHitSpeed = m_BallHitSpeedFactor.Evaluate(m_AnimationTime);
        }
    }

    private void MoveBall()
    {
        m_CurrentBallPosition +=
            m_MoveDirection
            * m_Speed
            * m_AdditionalHitSpeed
            * Time.deltaTime;

        transform.position = m_CurrentBallPosition;
    }

    private void CheckIfBallIsOutOfBounds()
    {
        var ballPosition = transform.position;

        if (Mathf.Abs(ballPosition.x) > 13)
        {
            // publish a message that a player scored
            var messagePlayerScored = new Message_PlayerScored { hits = m_PaddleHits, ballPositionX = ballPosition.x };
            MessageBus.Publish(messagePlayerScored);
            m_PaddleHits = 0;

            // published a message that the active player changed
            var messageActivePlayerChanged = new Message_ActivePlayerChanged
            {
                UpdatedActivePlayer = ballPosition.x < 0 ? PlayerLocations.Right : PlayerLocations.Left
            };
            MessageBus.Publish(messageActivePlayerChanged);
        }
    }
}
