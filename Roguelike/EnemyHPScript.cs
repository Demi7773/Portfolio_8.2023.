using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    public float enemyHP;
    public GameObject[] drops;

    public void LoseHP(float dmg)
    {
        enemyHP -= dmg;
        //Debug.Log("Enemy takes " + dmg + "dmg and has " + enemyHP + "hp");
        if (enemyHP <= 0f)
        {
            //Debug.Log("Enemy takes " + dmg + "dmg and dies");
            RollDrops();
            Destroy(gameObject);
        }
    }

    public void RollDrops()
    {
        int roll = Random.Range(0, 10) + 1;
        Debug.Log("Roll: " + roll);

        if (roll <= 5) 
        {
            Debug.Log("Bad drops test");
        }
        else if(roll >5)
        {
            Debug.Log("Good drops test");
            for (int i = 0; i < 2; i++)
            {
                Instantiate(drops[0], transform.position, Quaternion.identity);
            }
        }
    }
}
