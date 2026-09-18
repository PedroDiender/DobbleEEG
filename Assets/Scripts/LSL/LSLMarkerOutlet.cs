using LSL;
using UnityEngine;

public class LSLMarkerOutlet : MonoBehaviour
{
    public static LSLMarkerOutlet Instance { get; private set; }

    [Header("LSL Config")]
    public string streamName = "DobbleMarkers";
    public string streamType = "Markers";
    public int channelCount = 1;
    public double nominalRate = 0.0;

    private StreamOutlet _outlet;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        var info = new StreamInfo(
            streamName, streamType, channelCount, nominalRate,
            channel_format_t.cf_string, "dobble_markers_uid");

        _outlet = new StreamOutlet(info);
        Debug.Log($"[LSL] Outlet '{streamName}' pronto.");
    }

    private void OnDestroy()
    {
        _outlet?.Dispose();
    }

    private void Send(string marker)
    {
        if (_outlet == null) return;
        _outlet.push_sample(new string[] { marker });
        Debug.Log($"[LSL] -> {marker}");
    }

    public void SendGameStart(int mode)
        => Send($"Game_Start;mode={mode}");

    public void SendNewRound(CardData left, CardData right)
        => Send($"New_Round;left=[{string.Join(",", left.SymbolIds)}];right=[{string.Join(",", right.SymbolIds)}]");

    public void SendHit(int symbolId, float reactionMs)
        => Send($"Hit;symbol={symbolId};rt_ms={reactionMs:F1}");

    public void SendMiss(int symbolId)
        => Send($"Miss;symbol={symbolId}");

    public void SendGameOver(int hits, int misses, float elapsedSec, float avgSec)
        => Send($"Game_Over;hits={hits};misses={misses};total_s={elapsedSec:F2};avg_s={avgSec:F3}");
}