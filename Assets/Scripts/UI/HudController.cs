using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public TMP_Text txtHits, txtMisses, txtRemaining, txtTimer;

    public void UpdateHud(int hits, int misses, int remaining, float elapsed)
    {
        txtHits.text = hits.ToString();
        txtMisses.text = misses.ToString();
        txtRemaining.text = remaining.ToString();
        UpdateTimer(elapsed);
    }

    public void UpdateTimer(float elapsed)
    {
        int m = Mathf.FloorToInt(elapsed / 60f);
        int s = Mathf.FloorToInt(elapsed % 60f);
        txtTimer.text = $"{m:00}:{s:00}";
    }
}