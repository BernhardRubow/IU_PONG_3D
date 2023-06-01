using System.Collections;
using System.Collections.Generic;
using Assets.MainGame.Team.BR.Code.Interfaces;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public IMovement m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        m_Movement = GetComponent<IMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
