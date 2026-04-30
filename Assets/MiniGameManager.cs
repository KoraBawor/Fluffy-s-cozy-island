using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public float timeLimit = 5f;

    public Text timerText; // LEGACY TEXT
    public GameObject winScreen;
    public GameObject loseScreen;

    private float timer;
    private bool ended = false;

    void Start()
    {
        timer = timeLimit;

        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (ended) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            LoseGame();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timerText != null)
            timerText.text = "Czas: " + timer.ToString("0.0");
    }

    public void WinGame()
    {
        if (ended) return;

        ended = true;

        if (winScreen != null)
            winScreen.SetActive(true);
    }

    void LoseGame()
    {
        if (ended) return;

        ended = true;

        if (loseScreen != null)
            loseScreen.SetActive(true);
    }

    public bool IsGameEnded()
    {
        return ended;
    }

    public void BackToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
}
