using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    CharacterController charControl;

    private void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }
}
