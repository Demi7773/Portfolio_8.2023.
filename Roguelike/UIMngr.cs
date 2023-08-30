using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TestTools.CodeCoverage;
using UnityEngine;
using UnityEngine.UI;

public class UIMngr : MonoBehaviour
{
    [Header("Plug in Components")]
    public GameManager gm;
    public WeaponMngr weaponMngr;
    //public PewPew weaponScript;

    [Header("Plug in UI Elements")]
    public Text hpTxt;
    public Slider hpSlajd;
    public Text ammoTxt;
    public Text ammoTotalTxt;
    public Text moneyTxt;
    public Text potionNumberTxt;

    [Header("PickUp Panel")]
    public GameObject pickUpPanel;
    public Text pickUpTxtCurrent;
    public Image[] pickUpImgArray;
    public Image pickUpImgCurrent;

    [Header("Shop Panel")]
    public GameObject shopPanel;
    public Text moneyTxtShop;
    public GameObject itemOneUI;
    public GameObject itemTwoUI;
    public GameObject itemThreeUI;
    //public Image item1Image;
    //public Image item2Image;
    //public Image item3Image;
    public Text[] itemTxtArray;
    public Image[] itemImgArray;
    public int priceToBuyOne;
    public int priceToBuyTwo;
    public int priceToBuyThree;
    public Text price1Txt;
    public Text price2Txt;
    public Text price3Txt;


    //public GameObject reloadDisplay;
    //public Slider reloadSlajd;

    //public Text lvlTxt;
    //public Slider xpSlajd;

    [Header("Set UI values")]
    public float pickUpScreenDuration = 3f;
    public string[] pickUpTxtArray;


    private void Start()
    {
        // Updates for starting values on necessary displays
        hpDisplay();
        MoneyDisplay();
        //PotionDisplay();
    }

    private void Update()
    {
        hpDisplay();
        //xpDisplay();

        if (Input.GetKey(KeyCode.Escape))
        {
            ClosePanels();
        }
    }

    public void ClosePanels()
    {
        CloseShopPanel();
        pickUpPanel.SetActive(false);
    }

    // Updates current and max HP display, both text and slider. Called in update
    private void hpDisplay()
    {
        hpTxt.text = gm.currentHP + "/" + gm.maxHP;
        hpSlajd.maxValue = gm.maxHP;
        hpSlajd.value = gm.currentHP;
    }

    // Updates current and max ammo when called
    public void ammoDisplay(int ammoCurrent, int ammoMax)
    {
        ammoTxt.text = ammoCurrent + "/" + ammoMax;
    }

    public void ammoTotalDisplay(int ammoTotal)
    {
        ammoTotalTxt.text = ammoTotal.ToString();
    }

    // Updates current money display when called
    public void MoneyDisplay()
    {
        moneyTxt.text = "Money: " + gm.money + "$";
        moneyTxtShop.text = "Money: " + gm.money + "$";
    }

    public void PotionDisplay(int potionNum)
    {
        potionNumberTxt.text = potionNum.ToString();
    }


    // Displays the Pick up Panel and updates its components
    // Needs pickUpNumber input to determine what to display
    public void TurnOnPickUpD(int pickUpNum)
    {
        //Debug.Log("TurnOnPickUpD Started");
        StartCoroutine(PickUpDisplay(pickUpNum));
    }
    IEnumerator PickUpDisplay(int pickUpNumber)
    {
        //Debug.Log("PickUpDisplay Started");
        pickUpPanel.SetActive(true);
        
        if(pickUpNumber == 0)
        {
            pickUpTxtCurrent.text = pickUpTxtArray[pickUpNumber];
            pickUpImgCurrent = pickUpImgArray[pickUpNumber];
        }
        
        yield return new WaitForSeconds(pickUpScreenDuration);
        pickUpPanel.SetActive(false);
    }

    public void OpenShopPanel(int price1, int price2, int price3)
    {
        price1Txt.text = price1.ToString();
        price2Txt.text = price2.ToString();
        price3Txt.text = price3.ToString();
        shopPanel.SetActive(true);

        priceToBuyOne = price1;
        priceToBuyTwo = price2;
        priceToBuyThree = price3;
    }
    public void BuyItem1()
    {
        if(gm.money >= priceToBuyOne)
        {
            Debug.Log("Player Bought Item1 for " + priceToBuyOne + "!");
            // test with weapon1 for shop mechanic
            gm.UnlockWeapon(1);
            gm.LoseMoney(priceToBuyOne);
            itemOneUI.SetActive(false);
        }
    }
    public void BuyItem2()
    {
        if (gm.money >= priceToBuyTwo)
        {
            Debug.Log("Player Bought Item2 for " + priceToBuyTwo + "!");
            // test with weapon1 for shop mechanic
            gm.UnlockWeapon(2);
            gm.LoseMoney(priceToBuyTwo);
            itemTwoUI.SetActive(false);
        }
    }
    public void BuyItem3()
    {
        if (gm.money >= priceToBuyThree)
        {
            Debug.Log("Player Bought Item3 for " + priceToBuyThree + "!");
            // test with weapon1 for shop mechanic
            gm.UnlockWeapon(3);
            gm.LoseMoney(priceToBuyThree);
            itemThreeUI.SetActive(false);
        }
    }
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }
    
    //public void UpdateShopItems(Text[] itemTxt, Sprite[] itemImg, int shopSize)
    //{

    //}

    //private void xpDisplay()
    //{
    //    //if (gm.playerLvl == 0)
    //    //    {
    //    //        xpSlajd.maxValue = gm.lvlThreshold;
    //    //        xpSlajd.value = gm.xp;

    //    //        lvlTxt.text = " ";
    //    //    }
    //    //else 
    //    //    {
    //    //        xpSlajd.maxValue = gm.lvlThresholdIncrease;
    //    //        xpSlajd.value = gm.xpThisLvl;

    //    //        lvlTxt.text = "lvl " + gm.playerLvl;
    //    //    }
    //}


    //private void reloadUI()
    //{
    //    //reloadSlajd.maxValue = weaponMngr.reloadTimeSelected;
    //    //reloadSlajd.value = weaponMngr.reloadTajmerSelected;
    //}
}
