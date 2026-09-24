using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardRenderer : MonoBehaviour
{
    [Header("Prefab do símbolo")]
    public GameObject symbolPrefab;

    [Header("Configuração")]
    public float scaleFactor = 1f;

    private readonly List<GameObject> _spawned = new List<GameObject>();
    private readonly List<int> _symbolIds = new List<int>();
    private Action<int> _clickCallback;

    public void Render(CardData card, Action<int> onClick = null)
{
    Clear();
    _clickCallback = onClick;
    _symbolIds.Clear();

    for (int i = 0; i < card.SymbolIds.Length; i++)
    {
        int id = card.SymbolIds[i];
        Vector2 pos = CardLayout.Positions[i] * scaleFactor;
        float sizePx = (float)card.Sizes[i];
        float sizeUnits = sizePx * scaleFactor;

        var go = Instantiate(symbolPrefab, transform);
        go.name = $"Symbol_{id}";

        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(sizeUnits, sizeUnits);

        // Usa Image em vez de TMP_Text
        var img = go.GetComponent<Image>();
        if (img != null)
        {
            img.sprite = SymbolCatalog.Get(id);
            img.color = Color.white;
            img.preserveAspect = true;
        }
        else
        {
            Debug.LogWarning($"[CardRenderer] Image não encontrada no prefab para o símbolo {id}");
        }

        int captured = id;
        var btn = go.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => _clickCallback?.Invoke(captured));
        }

        _spawned.Add(go);
        _symbolIds.Add(id);
    }
}

    public IReadOnlyList<int> SymbolIds => _symbolIds;

    public void Clear()
    {
        foreach (var go in _spawned) Destroy(go);
        _spawned.Clear();
    }

    public RectTransform Rect => GetComponent<RectTransform>();
}