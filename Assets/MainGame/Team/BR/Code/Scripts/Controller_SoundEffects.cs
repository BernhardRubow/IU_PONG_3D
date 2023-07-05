using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Controller_SoundEffects : MonoBehaviour
{
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
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_PaddleHit>(OnPaddleHit);
        MessageBus.UnSubscribe<Message_SideLineHit>(OnSideLineHit);
    }


    // +++ custom event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnSideLineHit(object obj)
    {
        var clipToPlay = m_LineBounceSounds.GetRandom();
        m_AudioSource.PlayOneShot(clipToPlay);
    }

    private void OnPaddleHit(object eventArgs)
    {
        var clipToPlay = m_PaddleBounceSounds.GetRandom();
        m_AudioSource.PlayOneShot(clipToPlay);
    }
}