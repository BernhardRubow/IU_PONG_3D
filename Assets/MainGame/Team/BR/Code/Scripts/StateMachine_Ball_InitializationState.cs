using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

public class StateMachine_Ball_InitializationState : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // RandomPlayerServes
        var activePlayer =  Random.value < 0.5 ? PlayerLocations.Left : PlayerLocations.Right;
        var msg = new Message_ActivePlayerChanged
        {
            UpdatedActivePlayer = activePlayer
        };

        MessageBus.Publish<Message_ActivePlayerChanged>(msg);

        animator.SetBool("InitializationComplete", true);
    }


    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
