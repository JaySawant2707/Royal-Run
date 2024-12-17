using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;

    public bool GameOver => gameOver;

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        DecreaseTime();
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void IncreaseTimeLeft(float amount)
    {
        timeLeft += amount;
    }

    void DecreaseTime()
    {
        if (gameOver == true) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1") + "sec";

        if (timeLeft < 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
