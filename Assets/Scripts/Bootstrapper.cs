using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsurePersistentLoaded()
    {
        if (SceneManager.GetSceneByName("Persistent").isLoaded) return;

        bool alreadyLoading = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "Persistent")
                alreadyLoading = true;
        }

        if (!alreadyLoading)
        {
            SceneManager.LoadScene("Persistent", LoadSceneMode.Additive);
        }
    }
}