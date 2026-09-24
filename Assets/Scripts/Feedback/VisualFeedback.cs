using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VisualFeedback : MonoBehaviour
{
    public static VisualFeedback Instance { get; private set; }

    [Header("Cores")]
    public Color hitColor = new Color(0.18f, 0.80f, 0.44f, 1f);   
    public Color missColor = new Color(0.90f, 0.30f, 0.24f, 1f);  

    [Header("Animação")]
    public float hitDuration = 0.40f;
    public float hitScale = 0.15f;    
    public float missDuration = 0.30f;
    public float missShake = 12f;     

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void PlayHit(CardRenderer card) => StartCoroutine(PulseHit(card));
    public void PlayMiss(CardRenderer card) => StartCoroutine(ShakeMiss(card));

    private IEnumerator PulseHit(CardRenderer card)
    {
        var rt = card.Rect;
        var img = card.GetComponent<Image>();
        if (rt == null) yield break;

        Vector3 originalScale = rt.localScale;
        Color originalColor = img != null ? img.color : Color.white;

        if (img != null) img.color = hitColor;

        float t = 0f;
        while (t < hitDuration)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / hitDuration);
            float pulse = Mathf.Sin(p * Mathf.PI);   

            rt.localScale = originalScale * (1f + hitScale * pulse);
            if (img != null)
                img.color = Color.Lerp(hitColor, originalColor, p);

            yield return null;
        }

        rt.localScale = originalScale;
        if (img != null) img.color = originalColor;
    }

    private IEnumerator ShakeMiss(CardRenderer card)
    {
        var rt = card.Rect;
        var img = card.GetComponent<Image>();
        if (rt == null) yield break;

        Vector2 originalPos = rt.anchoredPosition;
        Color originalColor = img != null ? img.color : Color.white;

        if (img != null) img.color = missColor;

        float t = 0f;
        while (t < missDuration)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / missDuration);
            float decay = 1f - p;   

            float x = Mathf.Sin(t * 70f) * missShake * decay;
            float y = Mathf.Cos(t * 55f) * (missShake * 0.6f) * decay;
            rt.anchoredPosition = originalPos + new Vector2(x, y);

            if (img != null)
                img.color = Color.Lerp(missColor, originalColor, p);

            yield return null;
        }

        rt.anchoredPosition = originalPos;
        if (img != null) img.color = originalColor;
    }
}