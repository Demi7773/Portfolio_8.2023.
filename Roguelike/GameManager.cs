using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Plug In Components")]
    public UIMngr uiMngr;
    public WeaponMngr weaponMngr;

    [Header("Basic Attributes")]
    public float currentHP = 100;
    public float maxHP = 100;

    [Header("XP System - needs update")]
    public int xp = default;
    public int lvlThreshold = default;
    public int playerLvl;
    public float lvlScaling;
    public int flatXPIncrease;
    public int lvlThresholdIncrease;
    public int xpThisLvl;
    public int xpLastLvl = 0;

    [Header("Money System Test")]
    public int money;

    [Header("Spawn counter, difficulty - needs update")]
    public int spawnCount = default;
    public int diffy = default;

    // old declarations for reuse
    //public GameObject spawner;
    //SpawnerScaling spawn;

    private void Start()
    {
        currentHP = maxHP;
        //spawn = spawner.gameObject.GetComponent<SpawnerScaling>();
    }


    // hpPercent converted to flat and sent to GetHP()
    public void GetPercentHP(float healPercent)
    {
        float healConversion = healPercent * maxHP;
        GetHP(healConversion);
    }

    // GetHP adds healValue to hpCurrent capped at maxHP
    public void GetHP(float healValue)
    {
        currentHP += healValue;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // LoseHP handles Player taking dmg and checks if currentHP is over 0
    public void LoseHP(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Debug.LogError("GAME OVER");
        }
    }

    // GetMoney adds cash values to money and updates money UI
    public void GetMoney(int cash)
    {
        money += cash;
        uiMngr.MoneyDisplay();
    }

    // LoseMoney removes cash from money and updates money UI
    public void LoseMoney(int cash)
    {
        money -= cash;
        uiMngr.MoneyDisplay();
    }

    // UnlockWeapon sends msg to WeaponMngr to set bools active for weapon unlocks
    public void UnlockWeapon(int unlockWeaponNum)
    {
        weaponMngr.UnlockWeapon(unlockWeaponNum);
        uiMngr.TurnOnPickUpD(0);
    }

    //*****code for XP and Diffy systems, prolly have a better version around but can be salvaged*****

    //private void Update()
    //{
    //    //DifficultyBySpawn();
    //    //Xp();

    //    //xpThisLvl = xp - xpLastLvl;
    //}


    // Diffy tests
    //public void DifficultyBySpawn()
    //{
    //    if (spawnCount == 10 && diffy < 1)          // diffy 1
    //    {
    //        xp += 20;
    //        spawn.timerReset *= 0.9f;
    //        Debug.Log("Diffy 1, score: " + xp + ", spawnrate: " + spawn.timerReset);
    //        diffy = 1;
    //    }
    //    if (spawnCount == 25 && diffy < 2)          // diffy 2
    //    {
    //        xp += 50;
    //        spawn.timerReset *= 0.9f;
    //        Debug.Log("Diffy 2, score: " + xp + ", spawnrate: " + spawn.timerReset);
    //        diffy = 2;
    //    }
    //}



    // xp level up tests
    //public void Xp()
    //{
    //    if(xp>lvlThreshold)
    //    {
    //        xpLastLvl = lvlThreshold;
    //        playerLvl++;
    //        lvlThresholdIncrease = flatXPIncrease + (int)(lvlThreshold * lvlScaling);
    //        lvlThreshold += lvlThresholdIncrease;
    //        Debug.Log("Lvl up! " + playerLvl);
    //    }
    //    else
    //    {
    //        Debug.Log(xp);
    //    }
    //}
}
