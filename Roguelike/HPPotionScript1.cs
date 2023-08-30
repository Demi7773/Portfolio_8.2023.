using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HPPotionScript1 : MonoBehaviour
{
    public GameManager gm;
    public UIMngr uIMngr;
    public int numberOfHPPotions;
    public float potionHealAmount;
    public float potionCD;
    public bool potionIsOnCooldown = false;

    private void Start()
    {
        uIMngr.PotionDisplay(numberOfHPPotions);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)&& !potionIsOnCooldown && numberOfHPPotions > 0 && gm.currentHP < gm.maxHP)
        {
            numberOfHPPotions--;
            gm.GetHP(potionHealAmount);
            StartCoroutine(PotionCD());
        }
    }

    public void GetHpPotion1()
    {
        numberOfHPPotions++;
        uIMngr.PotionDisplay(numberOfHPPotions);
    }

    IEnumerator PotionCD()
    {
        potionIsOnCooldown = true;
        yield return new WaitForSeconds(potionCD);
        potionIsOnCooldown = false;
    }
}
