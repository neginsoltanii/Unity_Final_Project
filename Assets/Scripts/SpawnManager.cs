using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] pointsPrefabs;
    public GameObject[] rocksPrefabs;
    private GameManager gameManagerScript;

    private float spawnRangeX = 50;
    private float spawnPosY = 80;
    private float pointsStartDelay;
    [SerializeField] private float rocksStartDelay;
    private float elapsedTimeNextRock = 0f;

    //private float rocksSpawnInterval;
    //private bool setupRepeating = false; // Flag to set up repeating invocation only once
    //private float difficultyScore = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        Invoke("SpawnRandomPoints", pointsStartDelay);

  // Initial invocation for spawning rocks
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeNextRock = elapsedTimeNextRock + Time.deltaTime;

        float waitingTimeForSpawn = SetSpawnInterval( gameManagerScript.GetScore() );
        if (elapsedTimeNextRock > waitingTimeForSpawn)
        {
            elapsedTimeNextRock = 0f;
            Debug.Log("It has been" + waitingTimeForSpawn + " secs");
            SpawnRandomRocks();
        }


        // Check the game score and adjust rocksStartDelay and rocksSpawnInterval
        //if (gameManagerScript.GetScore() > difficultyScore)
        //{
        //    rocksSpawnInterval = 1.5f;

        //    //rocksSpawnInterval = SetSpawnInterval(gameManagerScript.GetScore());

        //    if (!setupRepeating) //if setup repeating is false
        //    {
        //        // Set up repeating invocation with adjusted delay and interval
        //        InvokeRepeating("SpawnRandomRocks", rocksStartDelay, rocksSpawnInterval);
        //        setupRepeating = true;
        //    }
        //}
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

        //if (gameManagerScript.GetScore() < difficultyScore)
        //{
        //    Invoke("SpawnRandomRocks", rocksStartDelay);
        //}
    }

    float SetSpawnInterval(int score)
    {
        float spawnInterval = (-0.08f * score) + 5;

        if (spawnInterval < 1)
        {
            return 1;
        }
        else
        {
            return spawnInterval;
        }

    }

}
