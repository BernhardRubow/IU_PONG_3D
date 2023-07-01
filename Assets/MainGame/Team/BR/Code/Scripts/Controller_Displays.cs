using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using TMPro;
using UnityEngine;

public class Controller_Displays : MonoBehaviour
{
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [SerializeField] private TextMeshProUGUI m_ServingStateDisplay;
    [SerializeField] private GameObject m_CenterLine;


    // +++ Unity event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnEnable()
    {
        MessageBus.Subscribe<Message_BallServing>(OnBallServing);
        MessageBus.Subscribe<Message_BallServed>(OnBallServed);
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_BallServing>(OnBallServing);
        MessageBus.UnSubscribe<Message_BallServed>(OnBallServed);
    }

    // +++ custom event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void OnBallServing(object eventArgs)
    {
        m_CenterLine.SetActive(false);
        var player = ((Message_BallServing)eventArgs).Player == PlayerLocations.Left ? 1 : 2;
        m_ServingStateDisplay.text = $"Player {player}\n press 'any key' \nto serve";
        m_ServingStateDisplay.enabled = true;
    }

    private void OnBallServed(object obj)
    {
        m_CenterLine.SetActive(true);
        m_ServingStateDisplay.enabled = false;
    }
}
