using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Controller_SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_PlayerScoredSounds;
    [SerializeField] private AudioClip[] m_PaddleBounceSounds;
    [SerializeField] private AudioClip[] m_LineBounceSounds;
    [SerializeField] private AudioSource m_AudioSource;

    // +++ Unity event functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        MessageBus.Subscribe<Message_PaddleHit>(OnPaddleHit);
        MessageBus.Subscribe<Message_SideLineHit>(OnSideLineHit);
        MessageBus.Subscribe<Message_PlayerScored>(OnPlayerScored);
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_PaddleHit>(OnPaddleHit);
        MessageBus.UnSubscribe<Message_SideLineHit>(OnSideLineHit);
        MessageBus.UnSubscribe<Message_PlayerScored>(OnPlayerScored);
    }


    // +++ custom event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnPlayerScored(object obj)
    {
        PlayRandomClip(m_PlayerScoredSounds);
    }

    private void OnSideLineHit(object obj)
    {
        PlayRandomClip(m_LineBounceSounds);
    }

    private void OnPaddleHit(object eventArgs)
    {
        PlayRandomClip(m_PaddleBounceSounds);
    }

    private void PlayRandomClip(AudioClip[] clipsToPlayFrom)
    {
        var clipToPlay = clipsToPlayFrom.GetRandom();
        m_AudioSource.PlayOneShot(clipToPlay);
    }
}