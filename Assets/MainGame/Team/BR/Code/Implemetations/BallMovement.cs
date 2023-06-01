using Assets.MainGame.Team.BR.Code.Enumerations;
using Assets.MainGame.Team.BR.Code.Interfaces;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Implemetations
{
    public class BallMovement : MonoBehaviour, IMovement
    {
        private Vector3 m_Direction;

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
                    if (m_Direction.y < 0) m_Direction.y *= -1;
                    break;

                case MoveDirections.Down:
                    if (m_Direction.y > 0) m_Direction.y *= -1;
                    break;

                case MoveDirections.Left:
                    if (m_Direction.x < 0) m_Direction.x *= -1;
                    break;

                case MoveDirections.Right:
                    if (m_Direction.x > 0) m_Direction.x *= -1;
                    break;
            }
        }
    }
}
