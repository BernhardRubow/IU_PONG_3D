using Assets.MainGame.Team.BR.Code.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_GlobalGameManager : MonoBehaviour
{
    public static Controller_GlobalGameManager Instance;

    [SerializeField] public GameTypes m_TypeOfGame = GameTypes.OnePlayerGame;

    // +++ Unity Event Handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
        DontDestroyOnLoad(this.gameObject);
    }

   
}
