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
    public int[] maxAmmunition = new int[6];
    public bool hasRKeycard;
    public bool hasGKeycard;
    public bool hasBKeycard;
    [Header("Pistol")]
    public GameObject pistolBulletPrefab;
    public float pistolCooldown;
    public int pistolMaxAmmo;
    [HideInInspector]public int pistolID = 2;

    [Header("Fist")]
    public GameObject fistBulletPrefab;
    public float fistCooldown;
    [HideInInspector]public int fistID = 1;

    int currentWeapon;
    float currentCooldown;
    int gunAmmo;
    private void Start()
    {
        ammunition[1] = 1;
        ammunition[2] = 50;

        maxAmmunition[2] = pistolMaxAmmo;
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
            UpdateValues(fistID, fistCooldown);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            UpdateValues(pistolID, pistolCooldown);
        }
        if (Input.GetMouseButton(0) && isReadyToShoot && gunAmmo > 0)
        {
            isReadyToShoot = false;
            gunAmmo--;
            switch (currentWeapon){
                case 1:
                    ShootFist();
                    ammunition[1] = gunAmmo;
                    print("fist");
                    break;
                case 2:
                    ShootPistol();
                    ammunition[2] = gunAmmo;
                    print("pistol");
                    break;
            }
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

        // stops player when punching
        if (player.rb.velocity.magnitude < 9f && player.rb.angularVelocity.magnitude < 9f)
        {
            Instantiate(fistBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            player.TempDisableVelocity(0.5f);
        }
        //else
        //{
        //    print(player.rb.velocity.magnitude + "|" + player.rb.angularVelocity.magnitude);
        //}

        // fist never loses ammo
        gunAmmo++;

        //audsrc.pitch = Random.Range(0.8f, 1.1f);
        //audsrc.PlayOneShot(bulletSound);
    }
    void ReadyToShoot()
    {
        isReadyToShoot = true;
    }
    void UpdateValues(int weapID, float weapCool)
    {
        currentWeapon = weapID;
        print("current weapon: " + weapID);
        currentCooldown = weapCool;
        gunAmmo = ammunition[weapID];
        ammoText.text = gunAmmo.ToString();
    }
    public void GetAmmo(int id, int amount)
    {
        int ammo = ammunition[id];
        int maxammo = maxAmmunition[id];
        float cool = 0;
        ammo += amount;
        ammo = Mathf.Max(ammo, 0); // not less than 0
        ammo = Mathf.Min(ammo, maxammo); // not more than maxhealth
        ammunition[id] = ammo;
        if (id == pistolID)
        {
            cool = pistolCooldown;
        }
        UpdateValues(id, cool);
    }

}
