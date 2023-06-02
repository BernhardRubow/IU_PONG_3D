using System;
using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;
using UnityEngine.Events;

public class GetPlayerInputComponent : MonoBehaviour
{
    [SerializeField]
    PlayerLocations _mPlayerLocationLocation = PlayerLocations.Left;
    
    Action m_Update;

    // Start is called before the first frame update
    void Start()
    {
        switch (_mPlayerLocationLocation)
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
        if(Input.GetKey(KeyCode.W)) transform.position += Vector3.up * Time.deltaTime;
        if(Input.GetKey(KeyCode.S)) transform.position += Vector3.down * Time.deltaTime;
    }

    void GetRightPlayerInput()
    {
        if (Input.GetKey(KeyCode.I)) transform.position += Vector3.up * Time.deltaTime;
        if (Input.GetKey(KeyCode.K)) transform.position += Vector3.down * Time.deltaTime;
    }
}
