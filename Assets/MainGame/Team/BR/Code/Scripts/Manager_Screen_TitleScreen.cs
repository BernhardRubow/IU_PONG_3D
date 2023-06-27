using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Screen_TitleScreen : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Global_GameSettings.TypeOfGame = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) Global_GameSettings.TypeOfGame = 2;

        if(Global_GameSettings.TypeOfGame == 1) SceneManager.LoadSceneAsync("OnePlayerGame");
        if(Global_GameSettings.TypeOfGame == 2) SceneManager.LoadSceneAsync("TwoPlayerGame");
    }
}