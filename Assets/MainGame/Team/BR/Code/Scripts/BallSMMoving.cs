using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using UnityEngine;

public class BallSMMoving : StateMachineBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private float m_AdditionalHitSpeed;

    [SerializeField]
    private Vector3 m_MoveDirection;

    [SerializeField]
    private AnimationCurve m_BallHitSpeedFactor;
    
    [SerializeField] 
    private float m_AnimationTime;

    [SerializeField] private Vector3 pos;

    private Transform m_Transform;

    public void OnEnable()
    {
        //Debug.Log("OnEnable");
        MessageBus.Subscribe<Message_SideLineHit>(OnSideLineHit);
        MessageBus.Subscribe<Message_PaddleHit>(OnPaddleHit);
    }

    public void OnDisable()
    {
        //Debug.Log("OnDisable");
        MessageBus.UnSubscribe<Message_SideLineHit>(OnSideLineHit);
    }

    // +++ Eventhandler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void OnSideLineHit(object eventArgs)
    {
        m_MoveDirection.y *= -1f;
    }

    private void OnPaddleHit(object eventArgs)
    {
        m_MoveDirection.x *= -1f;
        m_AnimationTime = 0;
        m_MoveDirection.y = m_MoveDirection.y = Random.Range(-1f, 1f); ;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Transform = animator.transform;
        pos = m_Transform.position;

        m_MoveDirection = animator.transform.position.x < 0 
            ? Vector3.right 
            : Vector3.left;
        m_MoveDirection.y = Random.Range(-1f, 1f);
        m_AnimationTime = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_AnimationTime < 2f)
        {
            m_AnimationTime += Time.deltaTime;
            m_AdditionalHitSpeed = m_BallHitSpeedFactor.Evaluate(m_AnimationTime);
        }

        pos +=
            m_MoveDirection
            * m_Speed
            * m_AdditionalHitSpeed
            * Time.deltaTime;

        m_Transform.position = pos;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
