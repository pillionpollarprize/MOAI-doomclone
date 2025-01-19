using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public int maxHealth = 100;
    public int maxArmor = 100;
    [HideInInspector] public int health;
    [HideInInspector] public int armor;
    void Start()
    {
        health = maxHealth;
        healthText.text = health + "%";

        armor = 0;
        armorText.text = armor + "%";
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0); // not less than 0
        health = Mathf.Min(health, maxHealth); // not more than maxhealth
        healthText.text = health + "%";
        if (health <= 0)
        {
            // todo: make death animation

            print("player dead :(");
            gameObject.SetActive(false);
        }
    }
}
