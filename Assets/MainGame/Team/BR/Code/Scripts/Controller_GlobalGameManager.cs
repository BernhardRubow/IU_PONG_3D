using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_GlobalGameManager : MonoBehaviour
{
    public static Controller_GlobalGameManager Instance;

    

    // +++ Unity Event Handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        MessageBus.Subscribe<Message_NewGameStarted>(OnNewGameStarted);
        MessageBus.Subscribe<Message_GameOver>(OnGameOver);
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_NewGameStarted>(OnNewGameStarted);
        MessageBus.UnSubscribe<Message_GameOver>(OnGameOver);
    }

    // +++ Custom Event Handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void OnNewGameStarted(object eventArgs)
    {
        var ea = (Message_NewGameStarted)eventArgs;
        if (ea.GameType == GameTypes.OnePlayerGame)
        {
            SceneManager.LoadScene("OnePlayerGame");
        }
        else
        {
            SceneManager.LoadScene("TwoPlayerGame");
        }
    }

    private void OnGameOver(object eventArgs)
    {
        var ea = (Message_GameOver)eventArgs;
        if (ea.WinnigPlayer == PlayerLocations.Left) SceneManager.LoadScene("PlayerOneWins");
        if (ea.WinnigPlayer == PlayerLocations.Right) SceneManager.LoadScene("PlayerTwoWins");
    }
}
