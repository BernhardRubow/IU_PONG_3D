using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_WinningScreen : MonoBehaviour
{
    // +++ Unity Event Handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnEnable()
    {
        
    }

    void OnDisable()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MessageBus.Publish(new Message_NewGameStarted { GameType = GameTypes.OnePlayerGame });
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MessageBus.Publish(new Message_NewGameStarted { GameType = GameTypes.TwoPlayerGame });
        }
    }
}
