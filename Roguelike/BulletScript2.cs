using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript2 : MonoBehaviour
{
    [Header("Plug in components")]
    public Rigidbody rb;
    public EnemyHPScript enemyHPS;
    [Header("Bullet stats")]
    public float bulletSpeed;
    public float dmg = 10;
    public float bulletDespawnTime = 3f;

    private void Start()
    {
        StartCoroutine(DespawnBullet());
        Vector3 targetDir = transform.rotation * Vector3.forward;
        rb.AddForce(targetDir * Time.deltaTime * bulletSpeed, ForceMode.VelocityChange);
    }

    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(bulletDespawnTime);
        //Debug.Log("Bullet despawned after " + bulletDespawnTime + " sec");
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Bullet hit Enemy");
            enemyHPS = other.GetComponent<EnemyHPScript>();
            enemyHPS.LoseHP(dmg);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            //Debug.Log("Bullet hit Wall");
            Destroy(this.gameObject);
        }
    }
}
