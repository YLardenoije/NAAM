using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector: MonoBehaviour
{
    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenename));
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(0);
    }
}
