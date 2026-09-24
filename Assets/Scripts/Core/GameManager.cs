using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Referências UI")]
    public CardRenderer cardLeft;
    public CardRenderer cardRight;
    public HudController hud;
    public EndGameScreen endScreen;

    [Header("Estado")]
    public int deckSize = 55;

    private List<CardData> _pile;
    private CardData _currentCard;
    private CardData _nextCard;
    private int _correctSymbolId = -1;

    private int _hits, _misses;
    private float _elapsed;
    private bool _running;

    private double _gameStartTime;
    private double _roundStartTime;

    private readonly List<float> _reactionTimes = new List<float>();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void StartGame(int mode)
    {
        deckSize = mode;
        _pile = FanoDeckGenerator.GenerateDeck(deckSize);

        _currentCard = _pile[_pile.Count - 1];
        _pile.RemoveAt(_pile.Count - 1);

        _hits = _misses = 0;
        _elapsed = 0f;
        _reactionTimes.Clear();
        _running = true;

        _gameStartTime = Time.realtimeSinceStartupAsDouble;

        hud.UpdateHud(_hits, _misses, _pile.Count, 0f);

        LSLMarkerOutlet.Instance.SendGameStart(deckSize);

        RenderRound();
    }

    private void Update()
    {
        if (!_running) return;
        _elapsed = (float)(Time.realtimeSinceStartupAsDouble - _gameStartTime);
        hud.UpdateTimer(_elapsed);
    }

    private void RenderRound()
    {
        if (_pile.Count == 0)
        {
            EndGame();
            return;
        }

        _nextCard = _pile[_pile.Count - 1];
        _pile.RemoveAt(_pile.Count - 1);

        _correctSymbolId = _currentCard.GetCommonSymbol(_nextCard);

        cardLeft.Render(_currentCard);
        cardRight.Render(_nextCard, OnSymbolClicked);

        hud.UpdateHud(_hits, _misses, _pile.Count, _elapsed);

        _roundStartTime = Time.realtimeSinceStartupAsDouble;

        LSLMarkerOutlet.Instance.SendNewRound(_currentCard, _nextCard);
    }

    private void OnSymbolClicked(int symbolId)
    {
        if (!_running) return;

        double reactionMs = (Time.realtimeSinceStartupAsDouble - _roundStartTime) * 1000.0;
        float reactionMsF = (float)reactionMs;

        if (symbolId == _correctSymbolId)
        {
            _hits++;
            _reactionTimes.Add(reactionMsF);

            VisualFeedback.Instance.PlayHit(cardRight);
            hud.UpdateHud(_hits, _misses, _pile.Count, _elapsed);

            LSLMarkerOutlet.Instance.SendHit(symbolId, reactionMsF);

            _currentCard = _nextCard;
            StartCoroutine(NextRoundDelayed(0.35f));
        }
        else
        {
            _misses++;

            VisualFeedback.Instance.PlayMiss(cardRight);
            hud.UpdateHud(_hits, _misses, _pile.Count, _elapsed);

            LSLMarkerOutlet.Instance.SendMiss(symbolId);
        }
    }

    private IEnumerator NextRoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        RenderRound();
    }

    private void EndGame()
    {
        _running = false;

        float avg = _reactionTimes.Count > 0
            ? _reactionTimes.Average() / 1000f
            : 0f;

        endScreen.Show(_elapsed, _hits, _misses, avg);

        LSLMarkerOutlet.Instance.SendGameOver(_hits, _misses, _elapsed, avg);
    }

    public void RestartGame() => StartGame(deckSize);
    public void BackToMenu() => SceneManager.LoadScene("Menu");
}