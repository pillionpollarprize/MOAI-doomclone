using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public Transform target;
    public bool canLookVertically;

    private void Start()
    {
        target = FindObjectOfType<PlayerMove>().transform;
        print(target != null ? "Player found" : "Player could not be found");
    }
    // the sprites always face the player
    void Update()
    { 
        if (canLookVertically)
        {
            transform.LookAt(target);
        }
        else
        {
            Vector3 targetY = target.position;
            targetY.y = transform.position.y;
            transform.LookAt(targetY);
        }
    }
}
