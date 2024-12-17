using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float ObstacleSpawnDelay = 2f;
    [SerializeField] float minObstaclaSpawnDelay = 0.2f;
    [SerializeField] float spawnWidth = 4f;

    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    public void DecreaseObstacleSpawnDelay(float amount)
    {
        ObstacleSpawnDelay -= amount;
        
        if (ObstacleSpawnDelay < minObstaclaSpawnDelay) ObstacleSpawnDelay = minObstaclaSpawnDelay;
        
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            Instantiate(obstacle, spawnPosition, Random.rotation);
            yield return new WaitForSeconds(ObstacleSpawnDelay);
        }

    }
}
