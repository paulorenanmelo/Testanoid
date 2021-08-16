using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    private static GamePlay _instance;
    public static GamePlay Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GamePlay>();
                if (_instance == null)
                {
#if DEBUG
                    Debug.LogError("Couldn't find GamePlay object.");
#endif
                    //var go = new GameObject("GamePlay");
                    //_instance = go.AddComponent<GamePlay>();
                    //DontDestroyOnLoad(_instance);
                }
            }

            return _instance;
        }
    }

    #region Delegates / events / actions
    public static event UnityAction onLoadingBegin;
    public static event UnityAction onLoadingComplete;
    public static event UnityAction<int> onScoreChanged;
    //public static event UnityAction<int> onLifeChanged;
    public static event UnityAction onGoal;

    // Wanna subscribe to one of the above events? Copy lines below to Start() and OnDestroy() respectively, or OnEnable() and OnDisable()
    //GamePlay.onLoadingComplete += MyListenerClass_LoadingCompleteMethod;
    //GamePlay.onLoadingComplete -= MyListenerClass_LoadingCompleteMethod;
    #endregion

    #region Getters / Setters
    public uint Score
    {
        get
        {
            return Stats.Instance.Score;
        }
        set
        {
            Stats.Instance.Score = value;
        }
    }
    public uint TotalScore
    {
        get
        {
            return Stats.Instance.TotalScore;
        }
        set
        {
            Stats.Instance.TotalScore = value;
        }
    }
    public uint Lives
    {
        get
        {
            return Stats.Instance.Lives;
        }
        set
        {
            Stats.Instance.Lives = value;
        }
    }
    public uint Briks
    {
        get
        {
            return Stats.Instance.Briks;
        }
        set
        {
            Stats.Instance.Briks = value;
        }
    }
    #endregion

    public BallController Ball;
    public PlayerController Player;

    private bool _gameOver = false;

    private IEnumerator StartGame()
    {
        if (onLoadingBegin != null)
            onLoadingBegin.Invoke();
        
        yield return new WaitForSeconds(3f);
#if DEBUG
        Debug.Log("Start Game");
#endif
        FindControllers();

        if (Lives <= 0)
        {
            Reset();
        }
        _gameOver = false;

        if(Ball != null)
            Ball.Kick();

        if (onLoadingComplete != null)
            onLoadingComplete.Invoke();
    }

    private void FindControllers()
    {
        if (Ball == null)
            Ball = FindObjectOfType<BallController>();
        if (Player == null)
            Player = FindObjectOfType<PlayerController>();
    }

    #region Unity methods
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        Reset();

        Goal();
    }

    private void Reset()
    {
        Score = 0;
        if(Lives <= 0)
            TotalScore = 0;
        Lives = 3;
        Briks = 4;
    }

    private void Update()
    {
#if DEBUG //debug commands
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Lives = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            TotalScore = Briks;
            Score = Briks;
        }
#endif

        if (_gameOver) return;

        if (Lives == 0)
        {
            _gameOver = true;
            SceneManager.LoadScene("Lose");
        }
        else if(Score == Briks)
        {
            _gameOver = true;
            SceneManager.LoadScene("Win");
        }
    }
    #endregion

    #region Public methods
    public void Scored(int scoreGained)
    {
        Score++; // if a brik requires multiple hits to destroy, then this will need to change
        TotalScore = (uint)Mathf.Max(TotalScore + scoreGained, 0); // if a brik has negative score, this ensures total score doesn't go below zero
        if (onScoreChanged != null)
            onScoreChanged.Invoke((int)TotalScore);
    }

    public void Goal()
    {
        if (onGoal != null)
            onGoal.Invoke();
#if DEBUG
        Debug.Log("Goal");
#endif
        FindControllers();

        Player.Reset();

        Ball.Reset();

        StartCoroutine(StartGame());
    }
    #endregion
}