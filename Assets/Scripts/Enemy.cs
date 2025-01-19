using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyState enemystate;
    public Transform target;
    public float speed;
    public int maxHealth = 100;
    
    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController walk;

    public GameObject deathObj;
    private Rigidbody rb;
    private Animator animator;
    private SpriteProjector spriteProj;
    private BoxCollider detect;

    int health;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteProj = GetComponent<SpriteProjector>();
        animator = GetComponentInChildren<Animator>();
        detect = GetComponent<BoxCollider>();
        health = maxHealth;
        enemystate = EnemyState.Idle;
    }
    private void Update()
    {
        animator.SetFloat("spriteRotation", spriteProj.lastIndex);
        if (enemystate == EnemyState.Idle)
        {
            // this sets the animator's controller
            animator.runtimeAnimatorController = idle;
        }
        if (enemystate == EnemyState.Walk)
        {
            animator.runtimeAnimatorController = walk;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetType().ToString().Equals("UnityEngine.BoxCollider") && col.gameObject.CompareTag("Player"))
        {
            enemystate = EnemyState.Walk;
        }
    }
    void FixedUpdate()
    {
        var targetRotation = target.position - transform.position;
        targetRotation.y = 0;

        Quaternion rotation = Quaternion.LookRotation(targetRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        if (enemystate == EnemyState.Walk)
        {
            var targetPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rb.MovePosition(targetPosition);
        }
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
public enum EnemyState
{
    Idle,
    Walk,
    Attack
}