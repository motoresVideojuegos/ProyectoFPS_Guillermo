using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Camera")]
    public float mouseSensibility;
    public float maxViewX;
    public float minViewX;
    public float rotationX;

    Camera camera;

    Rigidbody playerRb;

    PlayerClass plyClass;
    
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = Camera.main;

        plyClass = GetComponent<PlayerClass>();
        playerRb = GetComponent<Rigidbody>();

        plyClass.currentHealth = plyClass.maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CameraView();

        if(Input.GetButtonDown("Jump")){
            Jump();
        }
    }

    public void Movement(){
        float x = Input.GetAxis("Horizontal") * plyClass.velocity;
        float z = Input.GetAxis("Vertical") * plyClass.velocity;

        Vector3 direction = transform.right * x + transform.forward * z;

        direction.y = playerRb.velocity.y;

        playerRb.velocity = direction;
        playerRb.velocity.Normalize();
    }

    public void CameraView(){
        
        float y = Input.GetAxis("Mouse X") * mouseSensibility;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensibility;

        rotationX = Mathf.Clamp(rotationX, minViewX, maxViewX);

        camera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }

    public void Jump(){
        Ray hit = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(hit, 1.1f)){
            playerRb.AddForce(Vector3.up * plyClass.jumpForce, ForceMode.Impulse);
        }
    }
}
