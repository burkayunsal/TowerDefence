using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool  isRunning = false;
    

    public static void OnStartGame()
    {
        if (isRunning) return;

        UIManager.I.OnGameStarted();
        isRunning = true;
    }

    public static void OnLevelFailed()
    {
        isRunning = false;
        UIManager.I.OnFail();
    }
    
    public static void ReloadScene()
    {
        isRunning = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameIn");
    }
    
}
