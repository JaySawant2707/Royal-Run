using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] float chunkLength = 10;
    [SerializeField] float moveSpeed = 10;

    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnStartingChunks();
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float zPosition = CalculatePositionZ();
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, zPosition);
        GameObject newChunk = Instantiate(chunkPrefab, spawnPosition, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }

    void Update()
    {
        MoveChunk();
    }

    float CalculatePositionZ()
    {
        float zPosition;

        if (chunks.Count == 0)
        {
            zPosition = transform.position.z;
        }
        else
        {
            zPosition = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return zPosition;
    }

    void MoveChunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
