using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameManager gameManager;

    int score = 0;

    public void UpdateScore(int amount)
    {
        if (gameManager.GameOver) return;
        score += amount;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
