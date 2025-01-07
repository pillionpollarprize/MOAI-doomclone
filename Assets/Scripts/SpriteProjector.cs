using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteProjector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject look;

    public Sprite N, W, S, E;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        var directionToPlayer = (look.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;

        // normalize the angle to a range of [0, 360)
        if (angle < 0) angle += 360;

        if (angle >= 315 || angle < 45) // north
        {
            spriteRenderer.sprite = N;
        }
        else if (angle >= 45 && angle < 135) // east
        {
            spriteRenderer.sprite = E;
        }
        else if (angle >= 135 && angle < 225) // south
        {
            spriteRenderer.sprite = S;
        }
        else if (angle >= 225 && angle < 315) // west
        {
            spriteRenderer.sprite = W;
        }
        Billboard(spriteRenderer.transform, look);
    }

    private void Billboard(Transform thing, GameObject mainCamera)
    {
        var dir = mainCamera.transform.position - thing.position;
        var lookAtRotation = Quaternion.LookRotation(-dir, Vector3.up);
        thing.rotation = Quaternion.Euler(0, lookAtRotation.eulerAngles.y, 0);
    }
}
