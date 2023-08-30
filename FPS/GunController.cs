using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    [Header("Muzzle")]
    public Transform muzzle;

    [Header("Gun Stats")]
    public float fireRate = 0f;
    public float fireRateMax = 0.1f;
    public float dmg = 10f;
    public float speed = 50f;
    public int ammo = 20;
    public int ammoMax = 20;
    public float reloadTime = 1f;
    public float reloadTimeMax = 1f;
    public float aimBloom;

    [Header("Can Shoot Check")]
    public bool canShoot = true;
    public bool hasToReload = false;
    public bool isReloading = false;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera aimCamera;

    public UIMngr uiMngr;


    private void Start()
    {
        uiMngr.ammoTxt.text = ammo + " / " + ammoMax;
    }
    private void Update()
    {
        // *******WEAPON CONTROLS*******
        //if(canShoot && Input.GetButtonDown("Fire1") && !hasToReload)    // semi auto
        //{
        //    ShootBullet();
        //}
        if (canShoot && Input.GetButton("Fire1") && !hasToReload)       // full auto
        {
            ShootBullet();
        }
        else if (hasToReload && Input.GetButton("Fire1"))
        {
            isReloading = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            mainCamera.enabled = false;
            aimCamera.enabled = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            mainCamera.enabled = true;
            aimCamera.enabled = false;
        }
        if (Input.GetButtonDown("Reload") && ammo < ammoMax)
        {
            isReloading = true;
        }


        if(isReloading)
        {
            uiMngr.reloadBar.gameObject.SetActive(true);
            uiMngr.reloadBar.maxValue = reloadTimeMax;
            uiMngr.reloadBar.value = reloadTime;
            canShoot = false;
            reloadTime -= Time.deltaTime;

            if (reloadTime <= 0)
            {
                ReloadGun();
                reloadTime = reloadTimeMax;
            }
        }


        if(fireRate <= 0 && !isReloading)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        fireRate -= Time.deltaTime;
    }


    void ShootBullet()
    {
        float x = Screen.width / 2;
        float y = Screen.height / 2;

        float xAcc = Random.Range(x-aimBloom, x+aimBloom);
        float yAcc = Random.Range(y-aimBloom, y+aimBloom);

        var ray = Camera.main.ScreenPointToRay(new Vector3(xAcc, yAcc, 0));

        GameObject pew = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody bulletRB = pew.GetComponent<Rigidbody>();

        bulletRB.velocity = speed * ray.direction;
        Debug.Log(bulletRB.velocity);

        BulletScript bullet = pew.GetComponent<BulletScript>();
        if(bullet != null )
        {
            bullet.dmg = dmg;

        }

        ammo--;
        uiMngr.ammoTxt.text = ammo + " / " + ammoMax;

        if (ammo <= 0)
        {
            canShoot = false;
            hasToReload = true;
        }

        fireRate = fireRateMax;
    }

    void ReloadGun()
    {
        ammo = ammoMax;
        canShoot = true;
        isReloading = false;
        hasToReload = false;
        uiMngr.reloadBar.gameObject.SetActive(false);
        uiMngr.ammoTxt.text = ammo + " / " + ammoMax;
    }

    public void BulletHitEnemy(EnemyBehavior enemy)
    {
        if (enemy != null)
        {
            Debug.Log("bullethitenemy");
            enemy.LoseHP(dmg);
        }
    }
}
