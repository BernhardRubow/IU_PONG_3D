using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_GlobalGameManager : MonoBehaviour
{
    // Singleton
    public static Controller_GlobalGameManager Instance;
    [SerializeField] private int m_LeftPlayerScore;
    [SerializeField] private int m_RightPlayerScore;
    [SerializeField] private AudioSource m_BackgroundMusicAudioSource;
    [SerializeField] private AudioSource m_ApplauseAudioSource;

    public int m_WinningScore = 15;
    public PlayerLocations m_ActivePlayer = PlayerLocations.None;


    // +++ SUnity Event Handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            m_ActivePlayer = Random.Range(0, 1) == 0 ? PlayerLocations.Left : PlayerLocations.Right;
        }
        
    }

    void OnEnable()
    {
        MessageBus.Subscribe<Message_NewGameStarted>(OnNewGameStarted);
        MessageBus.Subscribe<Message_PlayerScored>(OnPlayerScored);
        MessageBus.Subscribe<Message_ActivePlayerChanged>(OnActivePlayerChanged);
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_NewGameStarted>(OnNewGameStarted);
        MessageBus.UnSubscribe<Message_PlayerScored>(OnPlayerScored);
        MessageBus.UnSubscribe<Message_ActivePlayerChanged>(OnActivePlayerChanged);
    }


    // +++ Custom Event Handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnActivePlayerChanged(object obj)
    {
        var ea = (Message_ActivePlayerChanged)obj;
        m_ActivePlayer = ea.UpdatedActivePlayer;
    }

    private void OnPlayerScored(object eventArgs)
    {
        var ea = (Message_PlayerScored)eventArgs;
        if (ea.ballPositionX < 0)
        {
            m_RightPlayerScore++;
        } 
        else
        {
            m_LeftPlayerScore++;
        }

        if (m_RightPlayerScore == m_WinningScore || m_LeftPlayerScore == m_WinningScore)
        {
            m_BackgroundMusicAudioSource.Stop();
            m_ApplauseAudioSource.Play();
            if( m_RightPlayerScore == m_WinningScore) SceneManager.LoadScene("PlayerTwoWins");
            if( m_LeftPlayerScore == m_WinningScore) SceneManager.LoadScene("PlayerOneWins");

            ResetSettingForNewGame();
        }

        
    }

    private void OnNewGameStarted(object eventArgs)
    {
        m_ApplauseAudioSource.Stop();
        var backgroundMusicPlaying = m_BackgroundMusicAudioSource.isPlaying;
        if(!backgroundMusicPlaying) m_BackgroundMusicAudioSource.Play(); 

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

    // +++ Member +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void ResetSettingForNewGame()
    {
        // Reset settings
        m_LeftPlayerScore = 0;
        m_RightPlayerScore = 0;

        SwitchActivePlayer();
    }

    private void SwitchActivePlayer()
    {
        m_ActivePlayer = m_ActivePlayer == PlayerLocations.Left
            ? PlayerLocations.Right
            : PlayerLocations.Left;
    }
}
