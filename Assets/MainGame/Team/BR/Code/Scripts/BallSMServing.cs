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


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEnter");
        m_BallGameObject = animator.gameObject;
        m_Paddles = GameObject.FindGameObjectsWithTag("paddle");

        // RandomPlayerServes
        m_ActivePlayer = Random.value > 0.5 ? -1 : 1;

        m_ServingPaddleTransform = m_ActivePlayer == -1 
            ? m_Paddles.Single(x => x.transform.position.x < 0).transform 
            : m_Paddles.Single(x => x.transform.position.x > 0).transform;

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyDown(KeyCode.Space)) animator.SetBool("Served", true);

        m_BallGameObject.transform.position = m_ServingPaddleTransform.position -
                                              m_BallServingOffset * m_ActivePlayer ;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
