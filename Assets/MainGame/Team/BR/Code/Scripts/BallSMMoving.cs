using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class BallSMMoving : StateMachineBehaviour
    {
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_AdditionalHitSpeed;
        [SerializeField] private Vector3 m_MoveDirection;
        [SerializeField] private AnimationCurve m_BallHitSpeedFactor;
        [SerializeField] private float m_AnimationTime;
        [SerializeField] private float m_AnimationTimeThreshold;
        [SerializeField] private Vector3 pos;
        [SerializeField] private int m_PaddleHits = 0;
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

            m_Transform = animator.transform;
            pos = m_Transform.position;

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
            m_PaddleHits = 0;
        }


        // +++ Custom Eventhandler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void OnSideLineHit(object eventArgs)
        {
            m_MoveDirection.y *= -1f;
        }

        private void OnPaddleHit(object eventArgs)
        {
            m_PaddleHits++;
            m_MoveDirection.x *= -1f;
            m_AnimationTime = 0;
            m_MoveDirection.y = m_MoveDirection.y = Random.Range(-1f, 1f); ;
            m_AnimationTimeThreshold = Random.Range(2f, 3f);

            m_MoveDirection += m_MoveDirection.normalized * m_AccelerationFactor;


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
                var msg = new Message_PlayerScored{hits = m_PaddleHits};
                if (ballPosition.x < 0)
                {
                    // right player scored
                    PongGameManager.Instance.m_ActivePlayer = 1;
                }
                else
                {
                    // left Player scored
                    PongGameManager.Instance.m_ActivePlayer = -1;
                }

                ballStateMachine.SetBool("OutOfBounds", true);

                MessageBus.Publish(msg);
            }
        }
    }
}
