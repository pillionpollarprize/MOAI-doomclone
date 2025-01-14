using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int maxHealth = 100;

    public GameObject deathObj;
    private Rigidbody rb;
    private Animator animator;
    private SpriteProjector spriteProj;

    int health;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteProj = GetComponent<SpriteProjector>();
        animator = GetComponentInChildren<Animator>();
        health = maxHealth;
    }
    private void Update()
    {
        animator.SetFloat("spriteRotation", spriteProj.lastIndex);
    }
    void FixedUpdate()
    {
        var targetRotation = target.position - transform.position;
        targetRotation.y = 0;

        Quaternion rotation = Quaternion.LookRotation(targetRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        var targetPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        rb.MovePosition(targetPosition);
        
    }
    public void TakeDamage(int damage)
    {

        health -= damage;
        health = Mathf.Max(health, 0); // not less than 0
        health = Mathf.Min(health, maxHealth); // not more than maxhealth

        if (health <= 0)
        {
            Instantiate(deathObj, transform.position, transform.rotation);
            SelfDestruct();
        }
    }
    void SelfDestruct()
    {
        //Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
