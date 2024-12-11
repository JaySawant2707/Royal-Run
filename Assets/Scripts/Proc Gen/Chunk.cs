using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] int maxCoinsToSpawn = 4;
    [SerializeField] float coinSeperationDistance = 0.5f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2 - 5f };
    
    List<int> avaliableLanes = new List<int> {0, 1, 2};

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, avaliableLanes.Count); //no of fences to spawn

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (avaliableLanes.Count <= 0) break;

            int selectedLane = SelectLane(avaliableLanes);

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }

    }

    void SpawnApple()
    {
        if (avaliableLanes.Count <= 0 || Random.value > appleSpawnChance) return;

            int selectedLane = SelectLane(avaliableLanes);

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    void SpawnCoins()
    {
        if (avaliableLanes.Count <= 0 || Random.value > coinSpawnChance) return;

        int coinsToSpawn = Random.Range(0, maxCoinsToSpawn); //no of coins to spawn

        int selectedLane = SelectLane(avaliableLanes);

        float endOfChunkZ = transform.position.z + coinSeperationDistance * 2f;
        
        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = endOfChunkZ - (i * coinSeperationDistance);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    int SelectLane(List<int> avaliableLanes)
    {
        int randomLaneIndex = Random.Range(0, avaliableLanes.Count);//select a random lane
        int selectedLane = avaliableLanes[randomLaneIndex];//select lane at randomLaneIndex from available lanes
        avaliableLanes.RemoveAt(randomLaneIndex);//remove selected lane from avaliable lanes
        return selectedLane;
    }
}
