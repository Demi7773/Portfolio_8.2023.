using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHPPotion1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HPPotionScript1 hpPotScript = other.GetComponent<HPPotionScript1>();
            hpPotScript.GetHpPotion1();
            Destroy(gameObject);
        }
    }
}
