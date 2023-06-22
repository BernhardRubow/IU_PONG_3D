using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraSoundFxController : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_BounceSounds;
    [SerializeField] private AudioSource m_AudioSource;

    // +++ Unity event functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        MessageBus.Subscribe<Message_PaddleHit>(OnPaddleHit);
    }

    void OnDisable()
    {
        MessageBus.UnSubscribe<Message_PaddleHit>(OnPaddleHit);
    }


    // +++ custom event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void OnPaddleHit(object eventArgs)
    {
        var clipToPlay = m_BounceSounds.GetRandom();
        m_AudioSource.PlayOneShot(clipToPlay);
    }
}