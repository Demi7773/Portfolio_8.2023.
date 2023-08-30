using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public float playerMaxHP;
    public float playerCurrentHP;
    public int score = 0;

    public GameObject deathScreen;
    public GameObject fpsController;
    public GameObject mainMenuCamera;
    public UIMngr uiMngr;
    public GunController gun;

    public List<GameObject> enemies = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject[] weapons;
    int enemySpawnNum;

    private void Start()
    {
        playerCurrentHP = playerMaxHP;
        InvokeRepeating("SpawnEnemy", 0f, 3f);
        SwitchWeapon(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
    }



    public void SpawnEnemy()
    {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        int randomEnemy = Random.Range(0, enemies.Count);
        Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        enemySpawnNum++;
    }

    public void LoseHP(float dmg)
    {
        playerCurrentHP -= dmg;

        if(playerCurrentHP > 0f)
        {
            uiMngr.FlashScreenRed();
        }
        else if (playerCurrentHP <= 0f)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        fpsController.SetActive(false);
        mainMenuCamera.SetActive(true);
        Debug.LogError("Game Over");
        deathScreen.SetActive(true);
        //CursorVisible(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public static void CursorVisible(bool isVisible)
    {
        Cursor.visible = isVisible;
        if (isVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void SwitchWeapon(int indexActive)
    {
    //    Debug.Log("SwitchWeapon " + indexActive + 1);
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
            //Debug.Log("Weapons switched off");
        }
        weapons[indexActive].SetActive(true);
        gun = weapons[indexActive].GetComponent<GunController>();
        uiMngr.reloadBar.gameObject.SetActive(false);
        uiMngr.ammoTxt.text = gun.ammo + " / " + gun.ammoMax;
    }

    public void AddScore(int add)
    {
        score += add;
        uiMngr.scoreTxt.text = "Score: " + score * 10;
        uiMngr.endScoreTxt.text = "End score: " + score * 10;
    }
}
