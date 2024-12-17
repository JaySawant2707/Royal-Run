using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Tooltip("in seconds(int)")]
    [SerializeField] int checkpointTimeExtension = 8;
    [Tooltip("Decrease obstacle spawn delay by this value everytime player hits checkpoint.")]
    [SerializeField] float obstacleDelayTimeAmount = 0.2f;
    [Tooltip("Increase fence spawn chance by this value everytime player hits checkpoint.")]

    string playerString = "Player";
    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTimeLeft(checkpointTimeExtension);
            obstacleSpawner.DecreaseObstacleSpawnDelay(obstacleDelayTimeAmount);
        }
    }
}
