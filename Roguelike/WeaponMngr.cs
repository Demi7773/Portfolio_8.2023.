using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponMngr : MonoBehaviour
{
    [Header("Plug in Components")]
    public GameManager gm;
    public UIMngr uiMngr;
    public PewPew pewpew;

    [Header("List of Weapons")]
    public List<GameObject> weaponList = new List<GameObject>();

    [Header("WeaponMngrs current selected")]
    public GameObject currentWeapon;
    public int currentWeaponNumber = 0;
    public int ammoCurrentSelected = 0;
    public int ammoMaxSelected = 0;
    public int ammoTotalSelected = 0;
    //public float reloadTimeSelected;
    //public float reloadTajmerSelected;


    [Header("Weapons Unlocked Bools Test")]
    public bool weapon0Unlocked = true;
    public bool weapon1Unlocked = false;
    public bool weapon2Unlocked = false;
    public bool weapon3Unlocked = false;


    private void Start()
    {
        SwitchWeapon(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponNumber != 0)
        {
            SwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeaponNumber != 1 && weapon1Unlocked)
        {
            SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeaponNumber != 2 && weapon2Unlocked)
        {
            SwitchWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && currentWeaponNumber != 3 && weapon3Unlocked)
        {
            SwitchWeapon(3);
        }
    }

    public void UnlockWeapon(int weaponNum)
    {
        if(weaponNum == 1)
        {
            weapon1Unlocked = true;
        }
        if (weaponNum == 2)
        {
            weapon2Unlocked = true;
        }
        if (weaponNum == 3)
        {
            weapon3Unlocked = true;
        }
    }
    public void SwitchWeapon(int weaponNumber)
    {
            currentWeaponNumber = weaponNumber;
            //pewpew = currentWeapon.GetComponentInChildren<PewPew>();

            // swap off all on list, swap on selected
            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].SetActive(false);
            }
            weaponList[weaponNumber].SetActive(true);

            currentWeapon = weaponList[weaponNumber];
            Debug.Log("Weapon set to weapon" + weaponNumber);

        //pewpew = currentWeapon.gameObject.GetType(PewPew);
            

            ammoCurrentSelected = pewpew.ammoCurrent;
            ammoMaxSelected = pewpew.ammoMax;
            ammoTotalSelected = pewpew.ammoTotal;
            uiMngr.ammoDisplay(ammoCurrentSelected, ammoMaxSelected);
            uiMngr.ammoTotalDisplay(ammoTotalSelected);

        //reloadTimeSelected = pewpew.reloadTime;
        //reloadTajmerSelected = pewpew.reloadTajmer;
    }
}
