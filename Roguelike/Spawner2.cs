using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    [Header("Plug in components")]
    public GameManager gm;
    public GameObject player;
    public DoorScript1 doorScript;

    [Header("Enemy List")]
    public List<GameObject> enemies;

    [Header("SpawnerStats")]
    public float timeBetweenSpawns;
    public int spawnCounter;
    public int spawnCounterMax;

    public bool isSpawning = false;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isSpawning)
        {
            StartCoroutine(SpawnStart());
            isSpawning = true;
        } 
    }

    IEnumerator SpawnStart()
    {
        while (spawnCounter < spawnCounterMax)
        {
            SpawnEnemy(gm.diffy);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy(int diffy)
    {
        if(diffy == 0)
        {
            Instantiate(enemies[Random.Range(0,enemies.Count)]);
            Debug.Log("Spawned enemy");
            spawnCounter++;
            gm.spawnCount++;

            if(spawnCounter >= spawnCounterMax)
            {
                doorScript.isLocked = false;
                Debug.Log("Unlock Door");

                this.gameObject.SetActive(false);
            }
        }
    }




}
