using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) GameSettings.TypeOfGame = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) GameSettings.TypeOfGame = 2;

        if(GameSettings.TypeOfGame == 1) SceneManager.LoadSceneAsync("OnePlayerGame");
        if(GameSettings.TypeOfGame == 2) SceneManager.LoadSceneAsync("TwoPlayerGame");
    }
}