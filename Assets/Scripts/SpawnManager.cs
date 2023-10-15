using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject[] pointsPrefabs;
    public GameObject[] rocksPrefabs;

    [SerializeField] private float rocksStartDelay;
    private float spawnRangeX = 50;
    private float spawnPosY = 80;
    private float pointsStartDelay;
    private float elapsedTimeNextRock = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("SpawnRandomPoints", pointsStartDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //Game Mechanics: Calculate the waiting time for the next rock spawn based on the player's current score
        float waitingTimeForSpawn = SetSpawnInterval(gameManagerScript.GetScore());
        elapsedTimeNextRock += Time.deltaTime;

        //Check if enough time has passed since the last rock was spawned
        if (elapsedTimeNextRock > waitingTimeForSpawn)
        {
            elapsedTimeNextRock = 0f;
            Debug.Log("It has been" + waitingTimeForSpawn + " secs");
            SpawnRandomRocks();
        }
    }

    void SpawnRandomPoints()
    {
        pointsStartDelay = Random.Range(3, 6);
        int pointsIndex = Random.Range(0, pointsPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, pointsPrefabs[pointsIndex].transform.position.z);

        if (!gameManagerScript.isGameOver)
        {
            Instantiate(pointsPrefabs[pointsIndex], spawnPos, pointsPrefabs[pointsIndex].transform.rotation);
        }

        Invoke("SpawnRandomPoints", pointsStartDelay);
    }

    void SpawnRandomRocks()
    {
        int rocksIndex = Random.Range(0, rocksPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, pointsPrefabs[rocksIndex].transform.position.z);

        if (!gameManagerScript.isGameOver)
        {
            Instantiate(rocksPrefabs[rocksIndex], spawnPos, rocksPrefabs[rocksIndex].transform.rotation);
        }
    }

    //Calculate the spawn interval based on the player's score
    float SetSpawnInterval(int score)
    {
        float spawnInterval = (-0.08f * score) + 5;

        if (spawnInterval < 0.3)
        {
            return 0.3f;
        }
        else
        {
            return spawnInterval;
        }

    }

}
