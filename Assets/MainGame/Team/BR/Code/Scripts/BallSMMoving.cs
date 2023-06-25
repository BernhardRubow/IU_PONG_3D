using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class BallSMMoving : StateMachineBehaviour
    {
        [SerializeField] private float m_BallStartSpeed;

        /// <summary>
        /// The current Speed of the ball
        /// </summary>
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_AdditionalHitSpeed;
        [SerializeField] private Vector3 m_MoveDirection;
        [SerializeField] private AnimationCurve m_BallHitSpeedFactor;
        [SerializeField] private float m_AnimationTime;
        [SerializeField] private float m_AnimationTimeThreshold;
        [SerializeField] private Vector3 pos;
        [SerializeField] private int m_HitTilBallDoubleSpeed;

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

        private Transform m_Transform;

        // +++ Monobehavior Event functions +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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


        // +++ StateMachineBehaviour Event functions ++++++++++++++++++++++++++++++++++++++++++++++++++
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Served", false);

            // reset paddle hits, which are use to
            // raise the ball speed in the game
            m_PaddleHits = 0;

            m_Transform = animator.transform;
            pos = m_Transform.position;

            m_Speed = m_BallStartSpeed;

            m_MoveDirection = animator.transform.position.x < 0 
                ? Vector3.right 
                : Vector3.left;
            m_MoveDirection.y = Random.Range(-1f, 1f);
            m_AnimationTime = 0f;

            m_AnimationTimeThreshold = Random.Range(2f, 3f);
        }

    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CalculateAndApplyAccelerationAfterHit();

            MoveBall();

            CheckIfBallIsOutOfBounds(animator);
        }


        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }


        // +++ Custom Eventhandler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void OnSideLineHit(object eventArgs)
        {
            m_MoveDirection.y *= -1f;
        }

        private void OnPaddleHit(object eventArgs)
        {
            // store velocity of ball an normalize it as preparation
            // for set random vertical speed after paddle hit
            var moveDirectionMagnitude = m_MoveDirection.magnitude;
            m_MoveDirection = m_MoveDirection.normalized;

            // ball gets faster with erery paddle hit
            var newSpeed = m_BallStartSpeed * ((m_PaddleHits / (float)m_HitTilBallDoubleSpeed) + 1f);
            m_Speed = newSpeed;
            Debug.Log(newSpeed);

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
            pos +=
                m_MoveDirection
                * m_Speed
                * m_AdditionalHitSpeed
                * Time.deltaTime;

            m_Transform.position = pos;
        }

        private void CheckIfBallIsOutOfBounds(Animator ballStateMachine)
        {
            var ballPosition = m_Transform.position;

            if (Mathf.Abs(ballPosition.x) > 13)
            {
                // publish a message that a player scored
                var messagePlayerScored = new Message_PlayerScored{hits = m_PaddleHits, ballPositionX = ballPosition.x};
                MessageBus.Publish(messagePlayerScored);

                // published a message that the active player changed
                var messageActivePlayerChanged = new Message_ActivePlayerChanged
                {
                    UpdatedActivePlayer = ballPosition.x < 0 ? 1 : -1
                };
                MessageBus.Publish(messageActivePlayerChanged);

                ballStateMachine.SetBool("OutOfBounds", true);
            }
        }
    }
}
