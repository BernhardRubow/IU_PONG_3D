using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using UnityEditor.VersionControl;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    public static PongGameManager Instance;
    public int m_ActivePlayer;

    // +++ Unity event functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
    }

    void OnEnable()
    {
        MessageBus.Subscribe<Message_ActivePlayerChanged>(OnActivePlayerChanged);
        MessageBus.Subscribe<Message_GameOver>(OnGameOver);
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_ActivePlayerChanged>(OnActivePlayerChanged);
        MessageBus.UnSubscribe<Message_GameOver>(OnGameOver);
    }


    // +++ MessageBus Eventhandler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void OnActivePlayerChanged(object eventArgs)
    {
        Message_ActivePlayerChanged msg = (Message_ActivePlayerChanged)eventArgs;

        m_ActivePlayer = msg.UpdatedActivePlayer;
    }

    private void OnGameOver(object obj)
    {
        // ToDo : Implement Game Over
        throw new System.NotImplementedException();
    }
}
