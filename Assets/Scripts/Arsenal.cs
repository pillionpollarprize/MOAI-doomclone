using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    [Header("General")]
    public Transform bulletSpawn;
    public bool isReadyToShoot;
    public TextMeshProUGUI ammoText;
    public int[] ammunition = new int[6];
    public bool hasRKeycard;
    public bool hasGKeycard;
    public bool hasBKeycard;
    [Header("Pistol")]
    public GameObject pistolBulletPrefab;
    public float pistolCooldown;
    public float pistolMaxAmmo;
    [HideInInspector]public int pistolID = 2;

    [Header("Fist")]
    public GameObject fistBulletPrefab;
    public float fistCooldown;
    [HideInInspector]public int fistID = 1;

    int currentWeapon;
    float currentCooldown;
    int gunAmmo;

    //InvokeRepeating("Shoot", 0, fireRate);
    private void Start()
    {
        ammunition[1] = 1;
        ammunition[2] = 50;
        gunAmmo = ammunition[2];
        isReadyToShoot = true; 
        currentWeapon = 2;
        currentCooldown = pistolCooldown;
        ammoText.text = gunAmmo.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = fistID;
            print("current weapon: 1");
            currentCooldown = fistCooldown;
            gunAmmo = ammunition[fistID];
            ammoText.text = gunAmmo.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            currentWeapon = pistolID;
            print("current weapon: 2");
            currentCooldown = pistolCooldown;
            gunAmmo = ammunition[pistolID];
            ammoText.text = gunAmmo.ToString();
        }
        if (Input.GetMouseButton(0) && isReadyToShoot && gunAmmo > 0)
        {
            isReadyToShoot = false;
            switch (currentWeapon){
                case 1:
                    ShootFist();
                    print("fist");
                    break;
                case 2:
                    ShootPistol();
                    print("pistol");
                    break;
            }
            gunAmmo--;
            ammoText.text = gunAmmo.ToString();
            Invoke("ReadyToShoot", currentCooldown);
        }
    }
    void ShootPistol()
    {
        
        Instantiate(pistolBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        //audsrc.pitch = Random.Range(0.8f, 1.1f);
        //audsrc.PlayOneShot(bulletSound);
    }
    void ShootFist()
    {
        var player = gameObject.GetComponent<PlayerMove>();
        if (player.rb.velocity.magnitude < 9f && player.rb.angularVelocity.magnitude < 9f)
        {
            Instantiate(fistBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            player.TempDisableVelocity(0.5f);
        }
        else
        {
            print(player.rb.velocity.magnitude + "|" + player.rb.angularVelocity.magnitude);
        }
        gunAmmo++;
        //audsrc.pitch = Random.Range(0.8f, 1.1f);
        //audsrc.PlayOneShot(bulletSound);
    }
    void ReadyToShoot()
    {
        isReadyToShoot = true;
    }
}
