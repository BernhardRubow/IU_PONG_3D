using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Scripts
{
    public class BallSensors : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            
            if (other.tag == "side-line")
            {
                //Debug.Log(other.tag);

                MessageBus.Publish(new Message_SideLineHit());
                
            }
            else if (other.tag == "paddle")
            {
                //Debug.Log(other.tag);

                MessageBus.Publish(new Message_PaddleHit());
                
            }
        }
    }
}
