using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_Initialization : MonoBehaviour
{
    void Start()
    {
        

        // RandomPlayerServes
        var activePlayer = Random.value < 0.5 ? PlayerLocations.Left : PlayerLocations.Right;
        var msg = new Message_ActivePlayerChanged
        {
            UpdatedActivePlayer = activePlayer
        };

        MessageBus.Publish<Message_ActivePlayerChanged>(msg);
        this.enabled = false;

        var nextState = this.GetComponent<BallState_Serving>();
        nextState.enabled = true;

    }
    

}
