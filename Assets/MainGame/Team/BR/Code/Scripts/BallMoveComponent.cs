using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Implemetations
{
    public class BallMoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_Direction;

        [SerializeField]
        private float m_Speed;

        [SerializeField]
        private float m_AdditionalHitSpeed;

        [SerializeField] 
        private AnimationCurve m_BallHitSpeedFactor;

        [SerializeField] private float m_AnimationTime = 3;
        

        public Vector3 Direction
        {
            get => m_Direction;
            set => m_Direction = value;
        }

        public void SwitchMoveDirection(MoveDirections moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirections.Up:
                    m_Direction.y *= -1;
                    break;

                case MoveDirections.Down:
                    m_Direction.y *= -1;
                    break;

                case MoveDirections.Left:
                    m_Direction.x *= -1;
                    m_AnimationTime = 0f;
                    
                    break;

                case MoveDirections.Right:
                    m_Direction.x *= -1;
                    m_AnimationTime = 0f;
                    break;
            }
        }

        public void Update()
        {
            if (m_AnimationTime < 2f)
            {
                m_AnimationTime += Time.deltaTime;
                m_AdditionalHitSpeed = m_BallHitSpeedFactor.Evaluate(m_AnimationTime);
            }

            transform.position += 
                m_Direction 
                * m_Speed 
                * m_AdditionalHitSpeed
                * Time.deltaTime;
        }
    }
}
