using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    /// <summary>
    /// Loads a specified scene
    /// </summary>
    /// <param name="pSceneName">Scene to load. (specified in inspector)</param>
    public void LoadScene(string pSceneName)
    {
        SceneManager.LoadScene(pSceneName);
    }

    /// <summary>
    ///  Unloaded a specified scene
    /// </summary>
    /// <param name="pSceneName">Scene to unload. (specified in inspector)</param>
    public void UnloadScene(string pSceneName)
    {
        SceneManager.UnloadSceneAsync(pSceneName);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
