using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PewPew : MonoBehaviour
{
    [Header("Plug in components")]
    public GameObject bullet;
    public Transform muzzlePoint;
    public UIMngr uiMngr;
    public WeaponMngr weaponMngr;

    public GameObject reloadDisplay;

    [Header("Gun Stats")]
    public int ammoCurrent;
    public int ammoMax;
    public int ammoTotal;

    //test for bulletspread
    public float ammoSpread;

    //public float pewTajmer = 1f;
    public float timeBetweenShots = 1f;

    //public float reloadTajmer = 1f;
    public float reloadTime = 1f;

    public float dmg = 10;

    [Header("Bools for shoot mechanics")]
    public bool canShoot = true;
    public bool isReloading = false;
    public bool hasToReload = false;


    private void Start()
    {
        ammoCurrent = ammoMax;
    }

    private void Update()
    {
        if (canShoot && Input.GetButton("Fire1"))
        {
            Shoot();
        }
        if (!isReloading && hasToReload && Input.GetButton("Fire1"))
        {
            //Debug.Log("Out of ammo + Shoot -> Reload");
            StartCoroutine(ReloadNew());
        }
        if (!isReloading && Input.GetButton("Reload"))
        {
            //Debug.Log("R key pressed, Reload started");
            StartCoroutine(ReloadNew());
        }
    }



    // Simple Shoot Mechanic should work, Coroutines make this much easier to read
    // also includes AmmoCheck to set bools if empty
    void Shoot()
    {
        if (ammoCurrent <= 0)
        {
            canShoot = false;
            hasToReload = true;
            //Debug.Log("Out of Ammo, has to reload");
        }
        else
        {
            ammoCurrent--;
            uiMngr.ammoDisplay(ammoCurrent, ammoMax);
            //Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);

            //Test for bulletspread
            Vector3 spawnRotation = new Vector3(muzzlePoint.rotation.x * Random.Range(-ammoSpread, ammoSpread), muzzlePoint.rotation.y * Random.Range(-ammoSpread, ammoSpread), muzzlePoint.rotation.z * Random.Range(-ammoSpread, ammoSpread));
            Instantiate(bullet, muzzlePoint.position, Quaternion.Euler(spawnRotation) * muzzlePoint.rotation);
            Debug.Log("Bullet spawn" + spawnRotation);

            StartCoroutine(ShotCD());
        }

    }

    // *****IEnumerator tests, seems more simple but might need separate timer for reload display. Not worried about UI right now but check later*****
    IEnumerator ShotCD()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
    IEnumerator ReloadNew()
    {
        // if ammoTotal of weapon is 0 then swap weapon and stop coroutine
        if (ammoTotal <= 0)
        {
            AllOutOfAmmo();
            yield return new WaitForEndOfFrame();
        }
        else
        {
            Debug.Log("Reload Started");
            isReloading = true;
            canShoot = false;
            //reloadDisplay.SetActive(true);
            yield return new WaitForSeconds(reloadTime);

            // test for ammo total system
            int differenceAmmoForReload = ammoMax - ammoCurrent;
            
            // if trying to reload when not enough for full clip, ammoCurrent gains remaining bullets and ammoTotal set to 0
            if (differenceAmmoForReload > ammoTotal)
            {
                ammoCurrent += ammoTotal;
                ammoTotal = 0;
            }
            // if there is more ammoTotal than needed for reload, Continue normally
            else
            {
                ammoTotal -= differenceAmmoForReload;
                //uiMngr.ammoTotalDisplay(ammoTotal);
                //uiMngr.ammoDisplay(ammoCurrent, ammoMax);
                ammoCurrent += differenceAmmoForReload;
            }
        }

        // bools for shoot mechanic
        hasToReload = false;
        canShoot = true;
        isReloading = false;
        // UI
        uiMngr.ammoDisplay(ammoCurrent, ammoMax);
        uiMngr.ammoTotalDisplay(ammoTotal);
        //reloadDisplay.SetActive(false);
        Debug.Log("Reload Finished");
    }

    void AllOutOfAmmo()
    {
        weaponMngr.SwitchWeapon(0);

        // getting out of range error, temp replaced with SwitchWeapon(0)
        //if (weaponMngr.currentWeaponNumber == weaponMngr.weaponList.Count)
        //{
        //    weaponMngr.SwitchWeapon(0);
        //}
        //else
        //{
        //    weaponMngr.SwitchWeapon(weaponMngr.currentWeaponNumber + 1);
        //}
    }


    // Old code, might be useful for UI otherwise just delete

    //private void PewCD()
    //{
    //    pewTajmer -= Time.deltaTime;
    //    if (pewTajmer <= 0)
    //    {
    //        pewTajmer = 0;
    //    }
    //}

    //private void Reload()
    //{
    //    if(reloadTajmer > 0)
    //    {
    //        reloadDisplay.SetActive(true);
    //        reloadTajmer -= Time.deltaTime;
    //    }

    //    if (reloadTajmer < 0 )
    //    {
    //        reloadDisplay.SetActive(false);
    //        reloadTajmer = 0;
    //        ammoCurrent = ammoMax;
    //    }
    //}
}
