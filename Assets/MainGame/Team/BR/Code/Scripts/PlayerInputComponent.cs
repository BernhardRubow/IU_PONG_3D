using System;
using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputComponent : MonoBehaviour
{
    [SerializeField]
    PlayerLocations 
        m_PlayerLocationLocation = PlayerLocations.Left;

    [SerializeField] 
    private PlayerMoveComponent m_MoveComponent;

    Action m_Update;

    // Start is called before the first frame update
    void Start()
    {
        switch (m_PlayerLocationLocation)
        {
            case PlayerLocations.Left:
                m_Update = GetLeftPlayerInput;
                break;

            case PlayerLocations.Right:
                m_Update = GetRightPlayerInput;
                break;

            default:
                Debug.LogError("No Player Type Set.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Update();
    }

    void GetLeftPlayerInput()
    {
        if(Input.GetKey(KeyCode.W)) m_MoveComponent.MoveUp();
        if(Input.GetKey(KeyCode.S)) m_MoveComponent.MoveDown();
    }

    void GetRightPlayerInput()
    {
        if (Input.GetKey(KeyCode.I)) m_MoveComponent.MoveUp();
        if (Input.GetKey(KeyCode.K)) m_MoveComponent.MoveDown();
    }
}