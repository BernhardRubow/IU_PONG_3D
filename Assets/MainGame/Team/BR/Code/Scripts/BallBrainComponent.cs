using Assets.MainGame.Team.BR.Code.Enumerations;
using Assets.MainGame.Team.BR.Code.Implemetations;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class BallBrainComponent : MonoBehaviour
    {
        private BallMoveComponent m_BallMoveComponent;

        // Start is called before the first frame update
        void Start()
        {
            m_BallMoveComponent = GetComponent<BallMoveComponent>();
        }

        // Update is called once per frame
        void Update()
        {
       
        }

        void OnTriggerEnter(Collider other)
        {
            
            if (other.tag == "side-line")
            {
                Debug.Log(other.tag);
                m_BallMoveComponent
                    .SwitchMoveDirection(
                        transform.position.y > 0 
                            ? MoveDirections.Down 
                            : MoveDirections.Up);
            }
            else if (other.tag == "paddle")
            {
                Debug.Log(other.tag);
                Debug.Log(transform.position.x);
                m_BallMoveComponent
                    .SwitchMoveDirection(
                        transform.position.x < 0 
                        ? MoveDirections.Right
                        : MoveDirections.Left);
            }
        }
    }
}
