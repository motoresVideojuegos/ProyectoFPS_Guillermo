using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    private bool isPlayer;
    public int maxCurrentAmmo;
    public int currentAmmo;
    public int maxBullets;
    public int maxWeaponAmmo;
    public float bulletVelocity;
    public float fireCad;
    public float fireVelocity;
    private float shootReload = 0f;
}
