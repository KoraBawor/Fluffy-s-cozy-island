using System.Collections;
using UnityEngine;

public class CatWorker : MonoBehaviour
{
    [Header("Tasks")]
    public TaskZone[] tasks;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite workingSprite;

    [Header("Floating Text")]
    public GameObject floatingTextPrefab;

    private SpriteRenderer sr;
    private TaskZone currentTask;
    private bool working;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError("Brak SpriteRenderer na kocie!");
            return;
        }

        PickNextTask();

        if (idleSprite != null)
            sr.sprite = idleSprite;
    }

    void Update()
    {
        if (working || currentTask == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTask.transform.position,
            moveSpeed * Time.deltaTime
        );

        float distance = Vector2.Distance(transform.position, currentTask.transform.position);

        if (distance < 0.1f && !working)
        {
            StartCoroutine(DoTask());
        }

        // flip kota
        Vector3 dir = currentTask.transform.position - transform.position;

        if (dir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    IEnumerator DoTask()
    {
        if (working) yield break;

        working = true;

        if (workingSprite != null)
            sr.sprite = workingSprite;

        yield return new WaitForSeconds(currentTask.workTime);

        MoneyManager.Instance.AddMoneyFromCat(currentTask.reward, gameObject);

        ShowFloatingText(currentTask.reward);

        if (idleSprite != null)
            sr.sprite = idleSprite;

        working = false;

        PickNextTask();
    }

    void PickNextTask()
    {
        if (tasks == null || tasks.Length == 0)
        {
            Debug.LogWarning("Brak TaskZone przypisanych do kota!");
            return;
        }

        currentTask = tasks[Random.Range(0, tasks.Length)];
    }

    void ShowFloatingText(int amount)
    {
        if (floatingTextPrefab == null) return;

        GameObject obj = Instantiate(floatingTextPrefab);
        obj.transform.position = transform.position + Vector3.up;

        FloatingText ft = obj.GetComponent<FloatingText>();

        if (ft != null)
            ft.Setup("+" + amount + "$");
    }
}