using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Tooltip("in seconds(int)")]
    [SerializeField] int checkpointTimeExtension = 8;

    string playerString = "Player";
    GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTimeLeft(checkpointTimeExtension);
        }
    }
}
