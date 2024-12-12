using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Transform chunkParent;
    [SerializeField] GameObject checkpointChunk;
    [SerializeField] GameObject[] chunkPrefabs;

    [Header("Level Settings")]
    [SerializeField] int checkpointInterval = 8;
    [Tooltip("amount of chunks we start with")]
    [SerializeField] int startingChunkAmount = 12;
    [Tooltip("Don't change this value unless chunk prefab size reflects")]
    [SerializeField] float chunkLength = 10;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float minMoveSpeed = 4;
    [SerializeField] float maxMoveSpeed = 20;
    [SerializeField] float minGravityZ = -20f;
    [SerializeField] float maxGravityZ = -8f;

    int chunksSpawned;

    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunk();
    }

    public void ChangeChunkMoveSpeed(float amount)
    {
        float newMoveSpeed = moveSpeed + amount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - amount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);

            cameraController.ChangeCameraFOV(amount);
        }
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        float zPosition = CalculatePositionZ();

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, zPosition);
        GameObject chunkToSpawn = SelectChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, spawnPosition, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);//Injecting dependency to Chunk class
        chunksSpawned++;
    }

    private GameObject SelectChunkToSpawn()
    {
        GameObject chunkToSpawn;
        if (chunksSpawned % checkpointInterval == 0 && chunksSpawned != 0)
        {
            chunkToSpawn = checkpointChunk;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
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
