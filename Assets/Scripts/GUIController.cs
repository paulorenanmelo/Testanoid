using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreLabel;
    [SerializeField] private TextMeshProUGUI LivesLabel;
    [SerializeField] private TextMeshProUGUI GetReadyLabel;

    void Start()
    {
        GamePlay.onGoal += GamePlay_onEndGame;
        GamePlay.onLoadingBegin += GamePlay_onLoadingBegin;
        GamePlay.onLoadingComplete += GamePlay_onLoadingComplete;
    }

    private void OnDestroy()
    {
        GamePlay.onGoal -= GamePlay_onEndGame;
        GamePlay.onLoadingBegin -= GamePlay_onLoadingBegin;
        GamePlay.onLoadingComplete -= GamePlay_onLoadingComplete;
    }
    private void Update()
    {
        ScoreLabel.text = GamePlay.Instance.TotalScore.ToString();
        LivesLabel.text = GamePlay.Instance.Lives.ToString();
    }
    private void GamePlay_onLoadingComplete()
    {
        GetReadyLabel.enabled = false;
    }

    private void GamePlay_onLoadingBegin()
    {
        GetReadyLabel.enabled = true;
    }

    private void GamePlay_onEndGame()
    {
        GetReadyLabel.enabled = true;
        ScoreLabel.text = GamePlay.Instance.TotalScore.ToString();
        LivesLabel.text = GamePlay.Instance.Lives.ToString();
    }
}
