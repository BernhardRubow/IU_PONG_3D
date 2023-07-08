using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_TitleScreen : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MessageBus.Publish(new Message_NewGameStarted{GameType = GameTypes.OnePlayerGame});
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MessageBus.Publish(new Message_NewGameStarted { GameType = GameTypes.TwoPlayerGame });
        }
    }

   
}