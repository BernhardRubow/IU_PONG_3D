using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.MainGame.Team.BR.Code.Classes.MessageBus;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

public class StateMachine_Ball_ServingState : StateMachineBehaviour
{
    [SerializeField] private int m_ActivePlayer;
    [SerializeField] private GameObject[] m_Paddles;
    [SerializeField] private Transform m_ServingPaddleTransform;
    [SerializeField] GameObject m_BallGameObject;
    [SerializeField] private Vector3 m_BallServingOffset;
    [SerializeField] private int m_OffsetFactor;



    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("OutOfBounds", false);

        Debug.Log("StateMachine_Ball_ServingState - OnStateEnter");
        m_BallGameObject = animator.gameObject;
        m_Paddles = GameObject.FindGameObjectsWithTag("paddle");

        m_ServingPaddleTransform = Controller_OnePlayerGame.Instance.m_ActivePlayer == PlayerLocations.Left
        ? m_Paddles.Single(x => x.transform.position.x < 0).transform
        : m_Paddles.Single(x => x.transform.position.x > 0).transform;

        m_OffsetFactor = Math.Sign(m_ServingPaddleTransform.position.x);

        MessageBus.Publish(new Message_BallServing { Player = Controller_OnePlayerGame.Instance.m_ActivePlayer });
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Served", true);
            MessageBus.Publish<Message_BallServed>(new Message_BallServed());
        }

        m_BallGameObject.transform.position = m_ServingPaddleTransform.position -
                                              m_BallServingOffset * m_OffsetFactor;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("StateMachine_Ball_ServingState - OnStateExit");
    }

}
