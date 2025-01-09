using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Rigidbody rb;
    private Animator animator;
    private SpriteProjector spriteProj;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteProj = GetComponent<SpriteProjector>();
        animator = GetComponentInChildren<Animator>();
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
}
