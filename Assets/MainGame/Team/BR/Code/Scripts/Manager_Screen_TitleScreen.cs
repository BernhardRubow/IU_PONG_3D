using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Screen_TitleScreen : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Controller_GlobalGameManager.Instance.m_TypeOfGame = GameTypes.OnePlayerGame;
            SceneManager.LoadScene("OnePlayerGame");

            MessageBus.Subscribe<Message_GameOver>(OnGameOverOnePlayerGame);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Controller_GlobalGameManager.Instance.m_TypeOfGame = GameTypes.TwoPlayerGame;
            SceneManager.LoadScene("TwoPlayerGame");
        }
    }

    // +++ Custom Event Handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnGameOverOnePlayerGame(object eventArgs)
    {
        MessageBus.UnSubscribe<Message_GameOver>(OnGameOverOnePlayerGame);

        var ea = (Message_GameOver)eventArgs;
        if (ea.WinnigPlayer == PlayerLocations.Left) SceneManager.LoadScene("PlayerOneWins");
        if (ea.WinnigPlayer == PlayerLocations.Right) SceneManager.LoadScene("PlayerTwoWins");
    }
}