using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainHealScript1 : MonoBehaviour
{
    public GameManager gm;
    public bool playerIsInRange = false;
    public float fountainPercentHeal = 0.2f;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }

    private void Update()
    {
        if (playerIsInRange && Input.GetKeyDown(KeyCode.F))
        {
            gm.GetPercentHP(fountainPercentHeal);
            gameObject.SetActive(false);
        }
    }
}
