using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal2DClick : MonoBehaviour
{
    public string sceneName;

    private void OnMouseDown()
    {
        LoadScene();
    }

    void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Nie przypisano sceny do portalu: " + gameObject.name);
        }
    }
}
