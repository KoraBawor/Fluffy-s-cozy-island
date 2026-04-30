using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float fadeSpeed = 2f;

    Text txt;
    CanvasGroup cg;

    void Awake()
    {
        txt = GetComponent<Text>();
        cg = GetComponent<CanvasGroup>();
    }

    public void Setup(string value)
    {
        txt.text = value;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float alpha = 1f;

        while (alpha > 0)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            alpha -= fadeSpeed * Time.deltaTime;
            cg.alpha = alpha;
            yield return null;
        }

        Destroy(gameObject);
    }
}