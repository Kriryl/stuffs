using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Lives : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float winTime = 300f;
    [SerializeField] private TextMeshProUGUI livesText;

    [SerializeField] private Canvas loseScreen;
    [SerializeField] private TextMeshProUGUI damageDone;
    [SerializeField] private TextMeshProUGUI kills;
    [SerializeField] private TextMeshProUGUI timeAlive;

    [SerializeField] private TextMeshProUGUI damageDoneHS;
    [SerializeField] private TextMeshProUGUI killsHS;
    [SerializeField] private TextMeshProUGUI timeAliveHS;

    public bool GameIsOver { get; private set; } = false;

    public int TotalLives { get { return lives; } set { lives = value; } }

    private void Start()
    {
        if (loseScreen)
        {
            loseScreen.gameObject.SetActive(false);
        }
        print($"TOTAL DAMAGE = {PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_DAMAGE)}");
        print(PlayerPrefs.GetInt(ScoreSaver.HIGH_SCORE_KILLS));
        print(PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_TIME_ALIVE));
    }

    private void Update()
    {
        CheckWinCondition();
        if (livesText)
        {
            DisplayLives();
        }
        CheckLives();
    }

    private void CheckWinCondition()
    {
        if (Time.timeSinceLevelLoad >= winTime)
        {
            Win();
        }
    }

    private void DisplayLives()
    {
        if (TotalLives == 1)
        {
            livesText.text = $"{lives} life remaining";
        }
        else
        {
            livesText.text = $"{lives} lives remaining";
        }
    }

    private void CheckLives()
    {
        if (TotalLives <= 0)
        {
            Lose();
        }
    }

    private void Win()
    {
        print("You Won!");
    }

    public void Lose()
    {
        GameIsOver = true;
        LoseScreen.CreateSummary();
        if (loseScreen)
        {
            loseScreen.gameObject.SetActive(true);
        }
        if (damageDone && kills && timeAlive)
        {
            damageDone.text = $"Total damage done: {LoseScreen.GetTotalDamage()}.";
            kills.text = $"Total kills: {LoseScreen.GetTotalKills()}.";
            timeAlive.text = $"Time Alive: {Mathf.Round(Time.timeSinceLevelLoad * 10) / 10} seconds.";
        }
        if (Time.realtimeSinceStartup > PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_TIME_ALIVE))
        {
            ScoreSaver.SetTimeAlive(Time.timeSinceLevelLoad);
        }
        if (damageDoneHS && killsHS && timeAliveHS)
        {
            damageDoneHS.text = $"High score: {PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_DAMAGE)}";
            killsHS.text = $"High score: {PlayerPrefs.GetInt(ScoreSaver.HIGH_SCORE_KILLS)}";
            timeAliveHS.text = $"High score: {Mathf.Round(PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_TIME_ALIVE) * 10f / 10)} secs";
        }
        Time.timeScale = 0f;
    }

    public void StartOver()
    {
        Time.timeScale = 1f;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
