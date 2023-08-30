using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    [Header("Plug in Components")]
    public GameManager gm;
    public UIMngr uiMngr;

    [Header("Bool for shop range")]
    public bool inShopRange = false;

    [Header("Int values for roll mechanic")]
    public int roll = -1;
    public int firstRoll = -1;
    public int secondRoll = -1;

    [Header("Shop Contents Arrays")]
    public GameObject[] shopContentsPotential;
    public GameObject[] shopContents;
    public string[] itemTxtArrayPotential;
    public Text[] itemTxtArray;
    //public Image[] itemImgArrayPotential;
    //public Image[] itemImgArray;
    public Sprite[] itemSpriteArray;
    public Sprite[] itemSpriteArrayPotential;
    public int[] itemPriceArray;
    public int[] itemPriceArrayPotential;

    [Header("Number of Items")]
    public int shopSize = 3;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        uiMngr = FindObjectOfType<UIMngr>();
        RollItems();
    }

    // checks if player is in active range for shop
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
                inShopRange = true;
            }
    }
    private void OnTriggerExit(Collider other)
    {
        inShopRange = false;
    }



    private void Update()
    {
        if(inShopRange && Input.GetKey(KeyCode.F))
        {
            OpenShop();
        }
    }

    void OpenShop()
    {
        uiMngr.OpenShopPanel(itemPriceArray[0], itemPriceArray[1], itemPriceArray[2]);
    }

    public void RollItems()
    {
        for (int i = 0; i < shopSize; i++)
        {
            roll = Random.Range(0, shopContentsPotential.Length);
            //Debug.Log("Roll " + roll);

            // reroll mechanic
            while (roll == firstRoll || roll == secondRoll)
            {
                Debug.Log("Rerolling");
                roll = Random.Range(0, shopContentsPotential.Length);
                Debug.Log("Rerolled " + roll);
            }
            SetShopItem(i);
        }
        //uiMngr.UpdateShopItems(itemTxtArray[itemslotNum], shopContents, shopSize);

        // set shop items mechanic
        void SetShopItem(int itemSlotNum)
        {
            //uiMngr.UpdateShopItems(itemTxtArray, itemImgArray, shopSize);
            UpdateShopItems(itemSlotNum);

            if (itemSlotNum == 0)
            {
                firstRoll = roll;
            }
            else if (itemSlotNum == 1)
            {
                secondRoll = roll;
            }
        }

        void UpdateShopItems(int itemSlotNum)
        {
            Debug.Log("SetShopItem(" + itemSlotNum + ")");
            int itemNum = roll;
            shopContents[itemSlotNum] = shopContentsPotential[itemNum];
            itemTxtArray[itemSlotNum].text = itemTxtArrayPotential[itemNum];
            itemPriceArray[itemSlotNum] = itemPriceArrayPotential[itemNum];
            //itemImgArray[itemSlotNum] = itemImgArrayPotential[itemNum];
            itemSpriteArray[itemSlotNum] = itemSpriteArrayPotential[itemNum];
        }
    }
}
