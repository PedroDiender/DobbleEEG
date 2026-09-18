using System.Collections;
using UnityEngine;

public class VisualFeedback : MonoBehaviour
{
    public static VisualFeedback Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void PlayHit(CardRenderer card) => StartCoroutine(PulseGreen(card.Rect));
    public void PlayMiss(CardRenderer card) => StartCoroutine(ShakeRed(card.Rect));

    private IEnumerator PulseGreen(RectTransform rt)
    {
        Vector3 original = rt.localScale;
        float t = 0f, dur = 0.35f;
        while (t < dur)
        {
            t += Time.deltaTime;
            float p = Mathf.Sin(t / dur * Mathf.PI);
            rt.localScale = original * (1f + 0.015f * p);
            yield return null;
        }
        rt.localScale = original;
    }

    private IEnumerator ShakeRed(RectTransform rt)
    {
        Vector2 original = rt.anchoredPosition;
        float t = 0f, dur = 0.25f;
        while (t < dur)
        {
            t += Time.deltaTime;
            float x = Mathf.Sin(t * 60f) * 8f * (1f - t / dur);
            float y = Mathf.Cos(t * 45f) * 5f * (1f - t / dur);
            rt.anchoredPosition = original + new Vector2(x, y);
            yield return null;
        }
        rt.anchoredPosition = original;
    }
}