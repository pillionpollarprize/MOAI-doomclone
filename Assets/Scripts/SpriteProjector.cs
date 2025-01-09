using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteProjector : MonoBehaviour
{
    public Transform player;
    public Vector3 targetPos;
    public Vector3 targetDir;

    private SpriteRenderer spriteRenderer;

    public float angle;
    public int lastIndex;
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        // flips sprite
        Vector3 tempScale = Vector3.one;
        if (angle > 0) tempScale.x *= -1f;
        spriteRenderer.transform.localScale = tempScale;

        lastIndex = GetIndex(angle);
    }


    private int GetIndex(float angle)
    {
        // front
        if (angle > -45f && angle <= 45f) return 0;

        // right
        if (angle > 45f && angle <= 135f) return 1;

        // back
        if (angle > 135f || angle <= -135f) return 2;

        // left
        if (angle > -135f && angle <= -45f) return 3;

        return lastIndex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
