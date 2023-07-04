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
    [SerializeField] private TextMeshProUGUI m_RallyCountDisplay;
    [SerializeField] private int m_RallyCount;


    // +++ Unity event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnEnable()
    {
        MessageBus.Subscribe<Message_BallServing>(OnBallServing);
        MessageBus.Subscribe<Message_BallServed>(OnBallServed);
        MessageBus.Subscribe<Message_PaddleHit>(OnPaddleHit);
        
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_BallServing>(OnBallServing);
        MessageBus.UnSubscribe<Message_BallServed>(OnBallServed);
        MessageBus.UnSubscribe<Message_PaddleHit>(OnPaddleHit);
    }


    // +++ custom event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnPaddleHit(object obj)
    {
        m_RallyCount++;
        UpdateRallyCountDisplay();
    }

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
        m_RallyCount = 1;
        UpdateRallyCountDisplay();
    }


    // +++ member +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private string GetRallyCountText()
    {
        return m_RallyCount.ToString("000");
    }

    private void UpdateRallyCountDisplay()
    {
        m_RallyCountDisplay.text = GetRallyCountText();
    }
}
