using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPickUpTest1 : MonoBehaviour
{
    public GameManager gm;
    public DoorScript1 doorScript;
    public int rewardNum = 1;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.UnlockWeapon(rewardNum);

            Debug.Log("Player picked up weapon + " + rewardNum + "!");
            doorScript.isLocked = false;
            Destroy(gameObject);
        }
    }
}
