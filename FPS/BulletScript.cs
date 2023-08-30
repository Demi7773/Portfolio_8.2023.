using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public float speed = 20f;
    public float lifeTime = 2f;
    public float dmg = 10f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBehavior>().LoseHP(dmg);
            //EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            //GunController gun = GetComponent<GunController>();
            //if (gun != null)
            //{
            //    gun.BulletHitEnemy(enemy);
            //}
        }
        else
        {
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
