using System;

[Serializable]
public class CardData
{
    public int[] SymbolIds;
    public FanoDeckGenerator.SymbolSize[] Sizes;

    public int GetCommonSymbol(CardData other)
    {
        foreach (var id in SymbolIds)
            if (Array.IndexOf(other.SymbolIds, id) >= 0)
                return id;
        return -1;
    }
}