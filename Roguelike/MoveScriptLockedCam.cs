using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScriptLockedCam : MonoBehaviour
{
    [Header("Plug In Components")]
    public CharacterController controller;
    public Transform groundCheck;
    public WeaponMngr weaponMngr;
    public Camera mainCam;
    public Camera minimapCam;
    public LayerMask groundMask;

    [Header("Player Stats")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = 0.65f;
    public float jumpHeight = 3f;
    public bool isGrounded; 

    [Header("Movement Values")]
    public Vector3 velocity;
    public Vector3 move;

    [Header("Camera Stats")]
    public float cameraHeightCurrent;
    public float cameraHeightNormal;
    public float cameraHeightZoomIn;
    public float cameraHeightZoomOut;

    [Header("Minimap Camera Stats")]
    public float minimapHeightCurrent;
    public float minimapHeightNormal;
    public float minimapHeightZoomIn;
    public float minimapHeightZoomOut;

    [Header("MathFTests")]
    //public float currentVelocity;
    public float smoothTime;
    public float maxSpeed;



    private void Start()
    {
        cameraHeightCurrent = cameraHeightNormal;
        minimapHeightCurrent = minimapHeightNormal;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(speed * Time.deltaTime * move);

        controller.Move(velocity * Time.deltaTime);

        SetMainCameraTargetHeight();
        SetMiniMapCameraTargetHeight();
        //Camera.main.transform.position = new Vector3(controller.transform.position.x, cameraHeightCurrent, controller.transform.position.z);
    }


    void SetMainCameraTargetHeight()
    {
        mainCam.transform.position = new Vector3(controller.transform.position.x, cameraHeightCurrent, controller.transform.position.z);
        
    }
    void SetMiniMapCameraTargetHeight()
    {
        minimapCam.transform.position = new Vector3(controller.transform.position.x, cameraHeightCurrent, controller.transform.position.z);
    }

    void CameraCrawlTest(float currentVelocity)
    {
        Vector3 currentPos = mainCam.transform.position;
        Vector3 targetPos = controller.transform.position;
        minimapCam.transform.position = new Vector3(Mathf.SmoothDamp(currentPos.x, targetPos.x, ref currentVelocity, smoothTime, maxSpeed),
                                                    Mathf.SmoothDamp(currentPos.y, targetPos.y, ref currentVelocity, smoothTime, maxSpeed),
                                                    Mathf.SmoothDamp(currentPos.z, targetPos.z, ref currentVelocity, smoothTime, maxSpeed));
    }
    
    public void ZoomOut()
    {

    }
    public void ZoomIn()
    {

    }
}
