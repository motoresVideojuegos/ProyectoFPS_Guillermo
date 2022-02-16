using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    PoolObjectController bulletPool;
    private Transform firePoint;
    public bool isPlayer;
    WeaponClass wpClass;
    // Start is called before the first frame update
    void Awake()
    {
        wpClass = GetComponent<WeaponClass>();
        bulletPool = GetComponent<PoolObjectController>();
        firePoint = wpClass.firePoint;

        isPlayer = false;
        if(GetComponent<PlayerController>()){
            isPlayer = true;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && wpClass.currentAmmo > 0){
            if(wpClass.shootReload >= wpClass.fireCad){
                if(wpClass.infiniteAmmo == false){
                    Fire();
                    --wpClass.currentAmmo;
                    wpClass.shootReload = 0;
                }else{
                    Reload();
                    Fire();
                    wpClass.shootReload = 0;
                }
            }

            wpClass.shootReload += wpClass.fireVelocity * Time.deltaTime;

        }

        if(isPlayer == true){
            if(Input.GetKeyDown(KeyCode.R)){
                if(wpClass.currentAmmo != wpClass.maxWeaponAmmo){
                    Reload();
                }
            }
        }
    }

    private void Fire(){
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
    }

    public void addAmmo(int amount){
        wpClass.maxCurrentAmmo = Mathf.Clamp(wpClass.maxCurrentAmmo + amount, 0, wpClass.maxBullets);
    }
}
