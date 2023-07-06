using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class Controller_ScoreBoard : MonoBehaviour
{
    [SerializeField] private int m_WinningScore = 0;
    [SerializeField] private int m_PlayerLeftScore = 0;
    [SerializeField] private int m_PlayerRightScore = 0;
    [SerializeField] private TextMeshProUGUI m_ScoreDisplayText;

    // +++ Unity event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void OnEnable()
    {
        MessageBus.Subscribe<Message_PlayerScored>(OnPlayerScored);
    }

    public void OnDisable()
    {
        MessageBus.UnSubscribe<Message_PlayerScored>(OnPlayerScored);
    }

    private void Start()
    {
        // Get the winning score from the Game Manager
        m_WinningScore = Controller_OnePlayerGame.Instance.m_WinningScore;
    }

    // +++ messagebus event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnPlayerScored(object eventArgs)
    {

        var msg = (Message_PlayerScored)eventArgs;

        // on which playerside is the ball gone out of bounds
        if (msg.ballPositionX.WhichPlayer() == PlayerLocations.Left)
        {
            m_PlayerRightScore++;
        }
        else
        {
            m_PlayerLeftScore++;
        }

        m_ScoreDisplayText.text = $"{m_PlayerLeftScore:00} : {m_PlayerRightScore:00}";

        if (m_PlayerLeftScore == m_WinningScore)
        {
            MessageBus.Publish<Message_GameOver>(new Message_GameOver{WinnigPlayer = PlayerLocations.Left});
        }

        if (m_PlayerRightScore == m_WinningScore)
        {
            MessageBus.Publish<Message_GameOver>(new Message_GameOver { WinnigPlayer = PlayerLocations.Left });
        }
    }
}
