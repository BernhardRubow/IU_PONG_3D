using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallSMServing : StateMachineBehaviour
{
    [SerializeField] private int m_ActivePlayer;
    [SerializeField] private GameObject[] m_Paddles;
    [SerializeField] private Transform m_ServingPaddleTransform;
    [SerializeField] GameObject m_BallGameObject;
    [SerializeField] private Vector3 m_BallServingOffset;


    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEnter");
        m_BallGameObject = animator.gameObject;
        m_Paddles = GameObject.FindGameObjectsWithTag("paddle");

            m_ServingPaddleTransform = PongGameManager.Instance.m_ActivePlayer == -1
            ? m_Paddles.Single(x => x.transform.position.x < 0).transform 
            : m_Paddles.Single(x => x.transform.position.x > 0).transform;
    }

    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyDown(KeyCode.Space)) animator.SetBool("Served", true);

        m_BallGameObject.transform.position = m_ServingPaddleTransform.position -
                                              m_BallServingOffset * PongGameManager.Instance.m_ActivePlayer ;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
