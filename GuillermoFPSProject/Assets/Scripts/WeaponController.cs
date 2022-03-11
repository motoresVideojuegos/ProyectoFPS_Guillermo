using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    PoolObjectController bulletPool;
    private Transform firePoint;
    public bool isPlayer;
    WeaponClass wpClass;
    public CanvasController canvas;

    private Material originalMat;
    public Rigidbody weaponRigidbody;
    public BoxCollider weaponBox;

    void Awake()
    {
        wpClass = GetComponent<WeaponClass>();
        
        bulletPool = GetComponent<PoolObjectController>();
        firePoint = wpClass.firePoint;

        isPlayer = false;
        if(GetComponentInParent<PlayerController>()){
            isPlayer = true;
            canvas.setCurrentAmmo(wpClass.currentAmmo);
        }       

        originalMat = GetComponent<Renderer>().material;
    }

    private void Start() {
        if(GetComponentInParent<PlayerController>()){
           canvas.setMaxAmmo(wpClass.maxCurrentAmmo);
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(wpClass.picked == true){
            
            if(isPlayer == true){
                if(Input.GetButton("Fire1") && wpClass.currentAmmo > 0){
                    if(wpClass.shootReload >= wpClass.fireCad){
                        if(wpClass.infiniteAmmo == false){
                            Fire();
                            --wpClass.currentAmmo;
                            wpClass.shootReload = 0;
                            if(isPlayer == true){
                                canvas.setCurrentAmmo(wpClass.currentAmmo);
                            }
                        }else{
                            Reload();
                            Fire();
                            wpClass.shootReload = 0;
                        }
            }

            wpClass.shootReload += wpClass.fireVelocity * Time.deltaTime;

            }

                if(Input.GetKeyDown(KeyCode.R)){
                    if(wpClass.currentAmmo != wpClass.maxWeaponAmmo){
                        Reload();
                    }
                }
            }
        }else{
            weaponBox.gameObject.SetActive(true);
            weaponRigidbody.isKinematic = false;
        }
        
    }

    public void Fire(){
        GameObject newBullet = bulletPool.getObject();

        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;

        newBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * wpClass.bulletVelocity;

    }

    private void Reload(){
        
        int ammoDiff = wpClass.maxWeaponAmmo - wpClass.currentAmmo;

        if(wpClass.maxCurrentAmmo - ammoDiff < 0){
            wpClass.currentAmmo += wpClass.maxCurrentAmmo;
            wpClass.maxCurrentAmmo = 0;
        }else{
            wpClass.maxCurrentAmmo -= ammoDiff;
            wpClass.currentAmmo += ammoDiff;
        }

        canvas.setCurrentAmmo(wpClass.currentAmmo);
        canvas.setMaxAmmo(wpClass.maxCurrentAmmo);
    }

    public void addAmmo(int amount){
        wpClass.maxCurrentAmmo = Mathf.Clamp(wpClass.maxCurrentAmmo + amount, 0, wpClass.maxBullets);
        canvas.setMaxAmmo(wpClass.maxCurrentAmmo);
    }

    public void restoreMat(){
        GetComponent<Renderer>().material = originalMat;
    }
}
