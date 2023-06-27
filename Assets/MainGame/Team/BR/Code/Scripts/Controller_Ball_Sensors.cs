using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class Controller_Ball_Sensors : MonoBehaviour
    {
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
                    MessageBus.Publish(new Message_PaddleHit());
                    break;
            }
        }
    }
}
