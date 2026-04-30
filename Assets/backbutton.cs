using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public string sceneName = "MainMap";

    public void GoBack()
    {
        SceneManager.LoadScene(sceneName);
    }
}