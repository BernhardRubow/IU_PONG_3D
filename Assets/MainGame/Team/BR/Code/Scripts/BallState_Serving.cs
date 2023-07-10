using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

public class BallState_Serving : MonoBehaviour
{
    
    [SerializeField] private GameObject[] m_Paddles;
    [SerializeField] private Transform m_ServingPaddleTransform;
    [SerializeField] private Vector3 m_BallServingOffset;
    [SerializeField] private int m_OffsetFactor;

    void OnEnable()
    {
        Debug.Log("BallState_Serving - OnEnable");
        
        m_Paddles = GameObject.FindGameObjectsWithTag("paddle");

        try
        {
            m_ServingPaddleTransform = Controller_GlobalGameManager.Instance.m_ActivePlayer == PlayerLocations.Left
                ? m_Paddles.Single(x => x.transform.position.x < 0).transform
                : m_Paddles.Single(x => x.transform.position.x > 0).transform;
        }
        catch (Exception e)
        {
            m_ServingPaddleTransform = m_Paddles.Single(x => x.transform.position.x < 0).transform;
        }


            
        

        m_OffsetFactor = Math.Sign(m_ServingPaddleTransform.position.x);

        MessageBus.Publish(new Message_BallServing { Player = Controller_GlobalGameManager.Instance.m_ActivePlayer });
    }

    void OnDisable()
    {
        Debug.Log("BallState_Serving - OnDisable");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("BallState_Serving - if");
            this.enabled = false;
            var nextState = GetComponent<BallState_Moving>();
            nextState.enabled = true;
            MessageBus.Publish<Message_BallServed>(new Message_BallServed());
        }

        this.gameObject.transform.position = m_ServingPaddleTransform.position -
                                              m_BallServingOffset 
                                              * m_OffsetFactor;
    }
}
