using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    [Header("General")]
    public Transform bulletSpawn;
    public bool isReadyToShoot;
    public int[] ammunition = new int[6];
    [HideInInspector] public int currentWeapon;
    [Header("Pistol")]
    public GameObject pistolBulletPrefab;
    public float pistolCooldown = 1f;
    public float pistolMaxAmmo = 200f;
    [HideInInspector]public int pistolID = 2;

    [Header("Fist")]
    public GameObject fistBulletPrefab;
    public float fistCooldown = 2;
    [HideInInspector]public int fistID = 1;


    float currentCooldown;
    int gunAmmo;

    //InvokeRepeating("Shoot", 0, fireRate);
    private void Start()
    {
        isReadyToShoot = true; 
        currentWeapon = 2;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("current weapon: 1");
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            print("current weapon: 2");
            currentWeapon = 2; 
        }
        if (Input.GetMouseButtonDown(0) && isReadyToShoot)
        {
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
            isReadyToShoot = false;
            Invoke("ReadyToShoot", pistolCooldown);
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
      
        //audsrc.pitch = Random.Range(0.8f, 1.1f);
        //audsrc.PlayOneShot(bulletSound);
    }
    void ReadyToShoot()
    {
        isReadyToShoot = true;
    }
}
