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
                    var go = new GameObject("GamePlay");
                    _instance = go.AddComponent<GamePlay>();
                }
            }

            return _instance;
        }
    }

    #region Delegates / events / actions
    public static event UnityAction onLoadingBegin;
    public static event UnityAction onLoadingComplete;
    public static event UnityAction<int> onScoreChanged;
    public static event UnityAction<int> onLifeChanged;
    public static event UnityAction onEndGame;

    // Wanna subscribe to one of the above events? Copy lines below to Start() and OnDestroy() respectively, or OnEnable() and OnDisable()
    //GameController.onLoadingComplete += MyListenerClass_LoadingCompleteMethod;
    //GameController.onLoadingComplete -= MyListenerClass_LoadingCompleteMethod;
    #endregion

    public BallController Ball;
    public PlayerController Player;

    public uint Score = 0;
    public uint Lives = 3;
    public uint Briks = 4;

    private bool _gameOver = false;

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);

        if (Ball == null)
            Ball = FindObjectOfType<BallController>();
        if (Player == null)
            Player = FindObjectOfType<PlayerController>();

        if (onLoadingBegin != null)
            onLoadingBegin.Invoke();

        _gameOver = false;

        if(Ball != null)
            Ball.Kick();
    }

    #region Unity methods
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (onLoadingBegin != null)
            onLoadingBegin.Invoke();
        Reset();
    }

    private void Reset()
    {
        Score = 0;
        Lives = 3;
        Briks = 4;

        Goal();
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
            Score = Briks;
        }
#endif

        if (_gameOver) return;

        if (Score == Briks)
        {
            SceneManager.LoadScene("Win");
            _gameOver = true;
        }
        else if (Lives == 0)
        {
            SceneManager.LoadScene("Lose");
            _gameOver = true;
        }
    }
    #endregion

    #region Public methods
    public void Scored(int scoreGained)
    {
        Score = (uint)Mathf.Max(Score + scoreGained, 0);
        if (onScoreChanged != null)
            onScoreChanged.Invoke((int)Score);
    }

    public void Goal()
    {
        if (onEndGame != null)
            onEndGame.Invoke();

        Debug.Log("Goal");

        var pos1 = Player.transform.position;
        pos1.x = 0f;
        Player.transform.position = pos1;

        Ball.transform.position = Vector3.zero;
        Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        StartCoroutine(StartGame());
    }
    #endregion
}