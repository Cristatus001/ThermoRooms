using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentBootstrap : MonoBehaviour
{
    public string firstSceneName = "MainMenu";

    void Start()
    {
        // Only load if MainMenu isn't already loaded (avoids duplicate loads)
        if (!SceneManager.GetSceneByName(firstSceneName).isLoaded)
        {
            SceneManager.LoadScene(firstSceneName, LoadSceneMode.Additive);
        }
    }
}