using UnityEngine;

public static class SymbolCatalog
{
    private const int Total = 57;
    private static Sprite[] _cache;

    private static void LoadCache()
    {
        _cache = new Sprite[Total];
        for (int i = 0; i < Total; i++)
        {
            _cache[i] = Resources.Load<Sprite>($"Symbols/{i}");
            if (_cache[i] == null)
                Debug.LogWarning($"[SymbolCatalog] Sprite {i} não encontrado em Resources/Symbols/");
        }
    }

    public static Sprite Get(int id)
    {
        if (_cache == null) LoadCache();
        return _cache[id];
    }
}