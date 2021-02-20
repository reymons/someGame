using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static Scene CurrentScene
    {
        get
        {
            return SceneManager.GetActiveScene();
        }
        private set { }
    }

    public static void ReloadGame()
    {
        SceneManager.LoadScene(CurrentScene.name);
    }
}
