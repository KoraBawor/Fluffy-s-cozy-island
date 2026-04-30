using UnityEngine;

public class TreeClicker : MonoBehaviour
{
    public Sprite[] treeStages;
    public MiniGameManager manager;

    private SpriteRenderer sr;
    private int currentStage = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (treeStages != null && treeStages.Length > 0)
            sr.sprite = treeStages[0];
    }

    void OnMouseDown()
    {
        if (manager == null) return;
        if (manager.IsGameEnded()) return;

        Debug.Log("Klik drzewo: " + currentStage);

        if (currentStage < treeStages.Length - 1)
        {
            currentStage++;
            sr.sprite = treeStages[currentStage];
        }
        else
        {
            manager.WinGame();
        }
    }
}
