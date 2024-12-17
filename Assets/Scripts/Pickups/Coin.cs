using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scorePoints = 10;
    ScoreManager scoreManager;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
    
    protected override void OnPickup(PlayerCollision playerCollision)
    {
        scoreManager.UpdateScore(scorePoints);
        playerCollision.PlayCoinSound();
    }
}
