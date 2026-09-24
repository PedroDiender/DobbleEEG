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
            "<b><size=140%>Fim de Jogo!</size></b>\n\n" +
            $"Tempo Total: <b>{m:00}:{s:00}</b>\n" +
            $"Acertos: <color=#2ecc71><b>{hits}</b></color>\n" +
            $"Erros: <color=#e74c3c><b>{misses}</b></color>\n\n" +
            $"<color=#f1c40f><b>Média por Jogada: {avgSeconds:F2}s</b></color>";
    }

    public void Hide() => panel.SetActive(false);
}