using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickUpScript : MonoBehaviour
{
    [Header("Inputs for movement on Y axis")]
    public float speed = 2f;
    public float height = 0.2f;
    public float adjustHeight;

    [Header("Inputs for rotation")]
    public float rotateSpeed = 50f;

    private void FixedUpdate()
    {
        HoverPickUp();
        RotatePickUp();
    }

    void HoverPickUp()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(speed * Time.time) * height) + adjustHeight, transform.position.z);
    }

    void RotatePickUp()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, Time.time * rotateSpeed , transform.rotation.z);
    }

}
