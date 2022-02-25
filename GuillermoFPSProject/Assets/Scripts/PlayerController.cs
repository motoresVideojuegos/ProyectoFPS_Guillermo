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

    private bool canMove;

    Camera camera;

    Rigidbody playerRb;

    public PlayerClass plyClass;
    public CanvasController canvas;
    public DeathMenuController deathMenu;

    public List<WeaponClass> weaponList;
 
    private int currentWeapon;
    
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = Camera.main;
        canMove = true;

        plyClass = GetComponent<PlayerClass>();
        playerRb = GetComponent<Rigidbody>();

        plyClass.currentHealth = plyClass.maxHealth;

        canvas.LifeBar(plyClass.currentHealth,plyClass.maxHealth);
        canvas.UpdateScore(plyClass.playerScore);

        currentWeapon = 0;
        weaponList[currentWeapon].gameObject.SetActive(true);

        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            Movement();
            CameraView();
            changeWeapons();

            if(Input.GetButtonDown("Jump")){
                Jump();
            }
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

    public void takeDmg(int dmgTaken){
        plyClass.currentHealth -= dmgTaken;
        canvas.LifeBar(plyClass.currentHealth, plyClass.maxHealth);
        if(plyClass.currentHealth <= 0){
            canMove = false;
            canvas.gameObject.SetActive(false);
            deathMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void addLife(int heal){
        plyClass.currentHealth = Mathf.Clamp(plyClass.currentHealth + heal, 0, plyClass.maxHealth);
        canvas.LifeBar(plyClass.currentHealth, plyClass.maxHealth);
    }

    public void addPoints(int points){
        plyClass.playerScore += points;
        canvas.UpdateScore(plyClass.playerScore);
    }

    public void changeWeapons(){
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            currentWeapon += 1;
            if(currentWeapon > weaponList.Count - 1){
                weaponList[currentWeapon - 1].gameObject.SetActive(false);
                currentWeapon = 0;
                weaponList[currentWeapon].gameObject.SetActive(true);
            }else{
                weaponList[currentWeapon - 1].gameObject.SetActive(false);
                weaponList[currentWeapon].gameObject.SetActive(true);
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0){
            currentWeapon -= 1;
            if(currentWeapon < 0){
                weaponList[0].gameObject.SetActive(false);
                currentWeapon = weaponList.Count - 1;
                weaponList[currentWeapon].gameObject.SetActive(true);
            }else{
                weaponList[currentWeapon + 1].gameObject.SetActive(false);
                weaponList[currentWeapon].gameObject.SetActive(true);
            }
        }

        canvas.setCurrentAmmo(weaponList[currentWeapon].currentAmmo);
        canvas.setMaxAmmo(weaponList[currentWeapon].maxCurrentAmmo);
    }
}
