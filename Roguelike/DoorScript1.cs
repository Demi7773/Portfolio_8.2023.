using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript1 : MonoBehaviour
{
    public GameObject door;
    public bool isLocked = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isLocked)
        {
            Debug.Log("Opening Door");
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        door.SetActive(false);
    }
}
