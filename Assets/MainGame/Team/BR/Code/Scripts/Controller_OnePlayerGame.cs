using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

public class Controller_OnePlayerGame : MonoBehaviour
{
    public static Controller_OnePlayerGame Instance;
    public PlayerLocations m_ActivePlayer;
    

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
            m_ActivePlayer = Random.Range(0, 1) == 0 ? PlayerLocations.Left : PlayerLocations.Right;
        }


    }

    void OnEnable()
    {
        MessageBus.Subscribe<Message_ActivePlayerChanged>(OnActivePlayerChanged);
        
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_ActivePlayerChanged>(OnActivePlayerChanged);
        
    }


    // +++ MessageBus Eventhandler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void OnActivePlayerChanged(object eventArgs)
    {
        Message_ActivePlayerChanged msg = (Message_ActivePlayerChanged)eventArgs;

        m_ActivePlayer = msg.UpdatedActivePlayer;
    }

   
}
