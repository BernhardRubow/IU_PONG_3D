using UnityEngine;
using UnityEngine.Events;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class ScoreboardSwitchComponent : MonoBehaviour
    {
        public UnityEvent m_BallGetsNear;
        public UnityEvent m_BallGetsFar;

        void Start()
        {
           // m_BallGetsNear.Invoke();
        }

        void OnTriggerEnter(Collider other)
        {

            if (other.tag == "ball")
            {
                Debug.Log($"Trigger Enter : {other.tag}");
                m_BallGetsNear.Invoke();
            }
        }

        void OnTriggerExit(Collider other)
        {
            Debug.Log($"Trigger Exit  : {other.tag}");
            if (other.tag == "ball") m_BallGetsFar.Invoke();
        }
    }
}
