using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScaling : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnTimer;
    public float timerReset;

    public float playerDistance;
    public GameObject player;
    public Vector3 playerPos;

    public GameManager gm;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > timerReset)
        {
            gm.spawnCount++;
            if(gm.diffy == 0)
            {
                RollSpawnDiffy0();
            }
            else if(gm.diffy == 1)
            {
                RollSpawnDiffy1();
            }

            spawnTimer = 0f;
        }
    }

    private void RollSpawnDiffy0()
    {
        int roll = Random.Range(0, 3);              // roll for enemy spawn
        Debug.Log("roll: " + roll);

        Vector3 spawnPos1 = new Vector3(Random.Range(-16.5f, -16.5f), 1f, Random.Range(-16.5f, -16.5f));        // roll for Spawn Position
        Vector3 spawnPos2 = new Vector3(Random.Range(-16.5f, -16.5f), 1f, Random.Range(-16.5f, -16.5f));
        //Debug.Log("Rolled x=" + spawnPos.x + " z=" + spawnPos.z);

        playerPos = player.transform.position;

        while (Mathf.Abs(Vector3.Distance(spawnPos1, playerPos)) < playerDistance)
        {
            Debug.Log("Rerolling");
            spawnPos1 = new Vector3(Random.Range(-16.5f, 16.5f), 1f, Random.Range(-16.5f, 16.5f));
            //Debug.Log("New Roll: " + spawnPos1);
        }
        while (Mathf.Abs(Vector3.Distance(spawnPos2, playerPos)) < playerDistance || Mathf.Abs(Vector3.Distance(spawnPos2, spawnPos1)) < playerDistance)
        {
            Debug.Log("Rerolling");
            spawnPos1 = new Vector3(Random.Range(-16.5f, 16.5f), 1f, Random.Range(-16.5f, 16.5f));
            //Debug.Log("New Roll: " + spawnPos2);
        }


        if (roll == 0)
        {
            SpawnZombie(spawnPos1);
            SpawnZombie(spawnPos2);
        }

        else if (roll == 1)
        {
            SpawnBigBoi(spawnPos1);
            SpawnZombie(spawnPos2);
        }
        else if (roll == 2)
        {
            SpawnBigBoi(spawnPos1);
            SpawnBigBoi(spawnPos2);
        }

    }

    private void RollSpawnDiffy1()
    {
        int roll = Random.Range(0, 4);              // roll for enemy spawn
        Debug.Log("roll: " + roll);

        Vector3 spawnPos1 = new Vector3(Random.Range(-16.5f, -16.5f), 1f, Random.Range(-16.5f, -16.5f));        // roll for Spawn Position
        Vector3 spawnPos2 = new Vector3(Random.Range(-16.5f, -16.5f), 1f, Random.Range(-16.5f, -16.5f));
        Vector3 spawnPos3 = new Vector3(Random.Range(-16.5f, -16.5f), 1f, Random.Range(-16.5f, -16.5f));
        //Debug.Log("Rolled x=" + spawnPos.x + " z=" + spawnPos.z);

        playerPos = player.transform.position;

        while (Mathf.Abs(Vector3.Distance(spawnPos1, playerPos)) < playerDistance)
        {
            Debug.Log("Rerolling");
            spawnPos1 = new Vector3(Random.Range(-16.5f, 16.5f), 1f, Random.Range(-16.5f, 16.5f));
            //Debug.Log("New Roll: " + spawnPos1);
        }
        while (Mathf.Abs(Vector3.Distance(spawnPos2, playerPos)) < playerDistance || Mathf.Abs(Vector3.Distance(spawnPos2, spawnPos1)) < playerDistance)
        {
            Debug.Log("Rerolling");
            spawnPos1 = new Vector3(Random.Range(-16.5f, 16.5f), 1f, Random.Range(-16.5f, 16.5f));
            //Debug.Log("New Roll: " + spawnPos2);
        }
        while (Mathf.Abs(Vector3.Distance(spawnPos3, playerPos)) < playerDistance || Mathf.Abs(Vector3.Distance(spawnPos3, spawnPos1)) < playerDistance 
               || Mathf.Abs(Vector3.Distance(spawnPos3, spawnPos2)) < playerDistance)
        {
            Debug.Log("Rerolling");
            spawnPos3 = new Vector3(Random.Range(-16.5f, 16.5f), 1f, Random.Range(-16.5f, 16.5f));
            //Debug.Log("New Roll: " + spawnPos2);
        }


        if (roll == 0)
        {
            SpawnZombie(spawnPos1);
            SpawnUniHorn(spawnPos2);
        }

        else if (roll == 1)
        {
            SpawnBigBoi(spawnPos1);
            SpawnUniHorn(spawnPos2);
        }
        else if (roll == 2)
        {
            SpawnBigBoi(spawnPos1);
            SpawnUniHorn(spawnPos2);
            SpawnZombie(spawnPos3);
        }
        else if (roll == 3)
        {
            SpawnUniHorn(spawnPos1);
            SpawnUniHorn(spawnPos2);
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

    private void SpawnUniHorn(Vector3 spawnPos)
    {
        Instantiate(enemies[2], new Vector3(spawnPos.x, spawnPos.y + 1f, spawnPos.z), Quaternion.identity);
    }
}
