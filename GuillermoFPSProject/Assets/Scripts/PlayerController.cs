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

    public Camera camera;

    Rigidbody playerRb;

    public PlayerClass plyClass;
    public CanvasController canvas;
    public DeathMenuController deathMenu;

    public List<WeaponClass> weaponList;
    public Transform weaponPosition;
 
    private int currentWeapon;
    RaycastHit hit;
    public Material rayHitMat;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;

        plyClass = GetComponent<PlayerClass>();
        playerRb = GetComponent<Rigidbody>();

        plyClass.currentHealth = plyClass.maxHealth;

        canvas.LifeBar(plyClass.currentHealth,plyClass.maxHealth);
        canvas.UpdateScore(plyClass.playerScore);

        currentWeapon = 0;
        weaponList[currentWeapon].gameObject.SetActive(true);
        initializeWeapons();

        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            Movement();
            CameraView();

            if(Input.GetAxis("Mouse ScrollWheel") > 0){
                changeWeapons();
            }
            
            if(Input.GetAxis("Mouse ScrollWheel") < 0){
                changeWeapons();
            }
            

            if(Input.GetButtonDown("Jump")){
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.Q)){
                throwWeapon();
            }

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1f)){
                if(hit.transform.GetComponent<WeaponClass>()){
                    GameObject hitGameobject = hit.transform.gameObject;

                    if(hit.transform.GetComponent<WeaponClass>().picked == false){
                        hitGameobject.GetComponent<Renderer>().material = rayHitMat;
                        if(Input.GetKeyDown(KeyCode.E)){
                            hitGameobject.GetComponent<WeaponController>().restoreMat();
                            pickUpWeapon(hit);
                        }
                    }
                }
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
        if(weaponList.Count != 0){
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
        }else{
            canvas.setCurrentAmmo(0);
            canvas.setMaxAmmo(0);
        }
 
    }

    public void pickUpWeapon(RaycastHit hit){
        GameObject weaponHit = hit.transform.gameObject;
        weaponHit.transform.parent = weaponPosition;
        weaponHit.transform.localRotation = Quaternion.Euler(0,0,0);
        weaponHit.transform.localPosition = new Vector3(0,0,0.5f);

        weaponHit.GetComponent<WeaponClass>().picked = true;
        weaponHit.SetActive(false);
        weaponList.Add(weaponHit.GetComponent<WeaponClass>());

    }

    public void throwWeapon(){
        int aux = currentWeapon;
        weaponList[aux].picked = false;
        weaponList[aux].transform.parent = null;
        currentWeapon += 1;
        if(currentWeapon > weaponList.Count - 1){
            currentWeapon = 0;
            weaponList[currentWeapon].gameObject.SetActive(true);
        }else{
            weaponList[currentWeapon].gameObject.SetActive(true);
        }
        weaponList.RemoveAt(aux);
    }

    public void initializeWeapons(){
        for(int i = 0; i<weaponList.Count; i++){
            weaponList[i].picked = true;
        }
    }

}
