using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadingScene
{
    public static string nextLvl;

    public static void LoadScene(string SceneName)
    {
        nextLvl = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
