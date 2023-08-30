using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 700f;
    public Transform playerBody;
    public float xRotation = 0f; // varijabla koja je odgovorna za rotaciju gore dolje
    //public float yRotation = 0f;     // test

    private void Start()
    {
        GameManager.CursorVisible(false);
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //      yRotation = mouseX;                        // test
        //transform.Rotate(mouseX, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY; // kod odgovoran za rotaciju kamere gore dolje, znaci oduzimamo vrijednost koju dobijemo
                             // od misa kako ga pomicemo od stvarne x kordinate na transformu
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ogranicavamo da mozemo ici samo 90 stupnjeva gore i 90 stupnjeva dolje
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // linija koda koja nam sluzi za postavljanje
        // lokalne rotacije objekta (najcesce kamere sto je i u nasem slucaju)
    }
}
