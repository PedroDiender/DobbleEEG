using LSL;
using UnityEngine;

public class LSLStreamInlet : MonoBehaviour
{
    [Header("Config")]
    public string targetStreamName = "";
    public string targetStreamType = "EEG";
    public float resolveTimeout = 5f;

    private StreamInlet _inlet;
    private float[] _sampleBuffer;
    private bool _resolved;

    public float LastAttention { get; private set; }
    public float LastEngagement { get; private set; }

    private void Start()
    {
        var results = LSL.LSL.resolve_stream(
            "type", targetStreamType, 1, resolveTimeout);

        StreamInfo info = null;
        foreach (var r in results) { info = r; break; }

        if (info == null)
        {
            Debug.LogWarning($"[LSL] Stream '{targetStreamType}' não encontrado.");
            return;
        }

        _inlet = new StreamInlet(info);
        _inlet.open_stream();
        _sampleBuffer = new float[info.channel_count()];
        _resolved = true;
        Debug.Log($"[LSL] Inlet conectado: {info.name()} ({info.channel_count()} canais)");
    }

    private void Update()
    {
        if (!_resolved) return;

        while (true)
        {
            double ts = _inlet.pull_sample(_sampleBuffer, 0.0);
            if (ts == 0.0) break;

            if (_sampleBuffer.Length >= 1) LastAttention = _sampleBuffer[0];
            if (_sampleBuffer.Length >= 2) LastEngagement = _sampleBuffer[1];
        }
    }

    private void OnDestroy()
    {
        _inlet?.Dispose();
    }
}