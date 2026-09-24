using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class FanoDeckGenerator
{
    public const int N = 7;
    public const int SymbolsPerCard = N + 1;
    public const int TotalSymbols = N * N + N + 1;

    public enum SymbolSize { Small = 26, Medium = 38, Large = 52 }

    private static readonly SymbolSize[] AllSizes =
        { SymbolSize.Small, SymbolSize.Medium, SymbolSize.Large };

    public static List<CardData> GenerateDeck(int limit)
    {
        var raw = new List<int[]>();

        var c0 = new int[SymbolsPerCard];
        c0[0] = 0;
        for (int i = 1; i <= N; i++) c0[i] = i;
        raw.Add(c0);

        for (int j = 0; j < N; j++)
        {
            var c = new int[SymbolsPerCard];
            c[0] = 0;
            for (int k = 0; k < N; k++)
                c[k + 1] = N + 1 + N * j + k;
            raw.Add(c);
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                var c = new int[SymbolsPerCard];
                c[0] = i + 1;
                for (int k = 0; k < N; k++)
                    c[k + 1] = N + 1 + N * k + ((i * k + j) % N);
                raw.Add(c);
            }
        }

        var limpas = raw.Where(c => c.Distinct().Count() == c.Length)
                        .OrderBy(_ => Random.value)
                        .Take(limit)
                        .ToList();

        return limpas.Select(ids => new CardData
        {
            SymbolIds = ids,
            Sizes = ids.Select(_ => AllSizes[Random.Range(0, AllSizes.Length)]).ToArray()
        }).ToList();
    }
}