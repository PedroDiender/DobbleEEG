using TMPro;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text txtSummary;

    public void Show(float elapsed, int hits, int misses, float avgSeconds)
    {
        panel.SetActive(true);
        int m = Mathf.FloorToInt(elapsed / 60f);
        int s = Mathf.FloorToInt(elapsed % 60f);
        txtSummary.text =
            $"🎉 Fim de Jogo! 🎉\n\n" +
            $"Tempo Total: {m:00}:{s:00}\n" +
            $"Acertos: {hits}\n" +
            $"Erros: {misses}\n" +
            $"⚡ Média por Jogada: {avgSeconds:F2}s";
    }

    public void Hide() => panel.SetActive(false);
}