using Assets.MainGame.Team.BR.Code.Enumerations;
using Assets.MainGame.Team.BR.Code.Implemetations;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class BallBrainComponent : MonoBehaviour
    {
        private BallMoveComponent m_BallMoveComponentScript;

        // Start is called before the first frame update
        void Start()
        {
            m_BallMoveComponentScript = GetComponent<BallMoveComponent>();
        }

        // Update is called once per frame
        void Update()
        {
       
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            if (other.tag == "side-line")
            {
                m_BallMoveComponentScript
                    .SwitchMoveDirection(
                        transform.position.y > 0 
                            ? MoveDirections.Down 
                            : MoveDirections.Up);
            }
        }
    }
}
