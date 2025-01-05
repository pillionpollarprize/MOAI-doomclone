using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int maxHealth = 100;
    bool isPlayer;
    [HideInInspector] public int health;
    void Start()
    {
        health = maxHealth;
        if (gameObject.CompareTag("Player")) 
        {
            isPlayer = true;
            healthText.text = health + "%";
        }
        else isPlayer = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0); // not less than 0
        health = Mathf.Min(health, maxHealth); // not more than maxhealth

        if (isPlayer)
        {
            healthText.text = health + "%";
        }

        if (health <= 0)
        {

            if (isPlayer)
            {
                // todo: make death animation
                print("player is kil");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
