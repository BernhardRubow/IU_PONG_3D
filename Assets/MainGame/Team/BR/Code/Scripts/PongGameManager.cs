using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    public static PongGameManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
