using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript1 : MonoBehaviour
{
    public GameManager gm;
    public int coinValue = 1;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.GetMoney(coinValue);
            //Debug.Log("Coin PickUp, money: " + gm.money);
            Destroy(transform.parent.gameObject);
        }
    }
}
