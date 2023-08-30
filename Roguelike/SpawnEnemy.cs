using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnTimer;
    public float timerReset;

    public float playerDistance;
    public GameObject player;
    public Vector3 playerPos;

    public GameManager gm;

    //private void Awake()
    //{
    //    gm = gameObject.GetComponent<GameManager>();
    //}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > timerReset)
        {
            gm.spawnCount++;
            RollSpawn();
            spawnTimer = 0f;
        }
    }

    private void RollSpawn()
    {
        int roll = Random.Range(0, enemies.Count);              // roll for enemy spawn             // tu kasnije dodati scenarije, promjeniti iz Count u scenarije
        //Debug.Log("roll: " + roll);

        Vector3 spawnPos = new Vector3(Random.Range(-15f, 15f), 1f, Random.Range(-15f, 15f));        // roll for Spawn Position
        //Debug.Log("Rolled x=" + spawnPos.x + " z=" + spawnPos.z);

        playerPos = player.transform.position;

        while (Mathf.Abs(Vector3.Distance(spawnPos, playerPos)) < playerDistance)
        {
            Debug.Log("Rerolling");
            spawnPos = new Vector3(Random.Range(-15f, 15f), 1f, Random.Range(-15f, 15f));
            Debug.Log("New Roll: " + spawnPos);
        }


        if (roll == 0)
        {
            SpawnZombie(spawnPos);
        }

        else if (roll == 1)
        {
            SpawnBigBoi(spawnPos);
        }

    }

    private void SpawnZombie(Vector3 spawnPos)
    {
        Instantiate(enemies[0], spawnPos, Quaternion.identity);
    }

    private void SpawnBigBoi(Vector3 spawnPos)
    {
        Instantiate(enemies[1], new Vector3(spawnPos.x, spawnPos.y + 1f, spawnPos.z), Quaternion.identity);
    }
}
