using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

public class Controller_MessageReceiver : MonoBehaviour
{
    public bool m_ComputerIsServing;

    void OnEnable()
    {
        MessageBus.Subscribe<Message_BallServing>(OnBallServing);
    }

    void OnDisable()
    {
        MessageBus.Subscribe<Message_BallServing>(OnBallServing);
    }

    private void OnBallServing(object eventArgs)
    {
        var ea = (Message_BallServing)eventArgs;
        if(ea.Player == PlayerLocations.Right) m_ComputerIsServing = true;
    }
}
