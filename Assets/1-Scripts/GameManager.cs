using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private Canvas gameOverCanvas, inGameCanvas, startGameCanvas;
    private int _playerCount = 1, _enemyCount = 1;
    private bool _isGameStarted = false;
    public static GameManager Instance
    {
        get => _instance;
        private set => _instance = value;
    }
    public int PlayerCount
    {
        get => _playerCount;
        //set => _playerCount = value;

        set
        {
            _playerCount = value;
            if (_playerCount <= 0)
            {
                Debug.Log("game over");
                GameOver();
            }
        }
    }
    public int EnemyCount
    {
        get => _enemyCount;
        set => _enemyCount = value;
    }
    public bool IsGameStarted => _isGameStarted;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }


    public void GameOver()
    {
        inGameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        _isGameStarted = false;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        gameOverCanvas.gameObject.SetActive(false);
        startGameCanvas.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartPlayingGame()
    {
        startGameCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
        _isGameStarted = true;
    }
}
