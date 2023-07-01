using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class Controller_Ball_Sensors : MonoBehaviour
    {
        [SerializeField] private int m_PaddleHits;


        // +++ Unity Event Handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
    }
}
