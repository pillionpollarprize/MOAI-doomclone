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
    public TextMeshProUGUI ammunitionPistolText;
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
        ammunitionPistolText.text = ammunition[2].ToString() + "/" + maxAmmunition[2].ToString();
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
                    UpdateValues(fistID, fistCooldown);
                    print("fist");
                    break;
                case 2:
                    ShootPistol();
                    ammunition[2] = gunAmmo;
                    UpdateValues(pistolID, pistolCooldown);
                    print("pistol");
                    break;
            }
            Invoke("ReadyToShoot", currentCooldown);
        }
    }
    void ShootPistol()
    {
        Instantiate(pistolBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
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
        ammunitionPistolText.text = ammunition[2].ToString() + "/" + maxAmmunition[2].ToString();
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
