using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpriteProjector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform plane;
    public GameObject look;

    private float step = 45f;

    public Sprite N, W, S, E;
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void Update()
    {
        // this uses a plane that points out a vector outwards from a point as if looking from the bottom to the top 
        // explaination: https://dustinanglin.medium.com/now-youre-just-projecting-7f93dbdc9378
        var projected = Vector3.ProjectOnPlane(look.transform.forward, plane.up);

        // this finds an angle between two vectors
        var angle = Vector3.SignedAngle(projected, plane.forward, plane.up);

        // absolute value so no minuses
        var AbsAngle = Mathf.Abs(angle);


        if (AbsAngle < step) spriteRenderer.sprite = N;
        else if (AbsAngle < step * 3) spriteRenderer.sprite = Mathf.Sign(angle) < 0 ? W : E;
        else spriteRenderer.sprite = S;
        Billboard(spriteRenderer.transform, look);
    }
    public void Billboard(Transform thing, GameObject mainCamera)
    {
        var dir = plane.position - mainCamera.transform.position;
        var LookAtRotation = Quaternion.LookRotation(dir);

        var LookAtRotationOnly_Y = Quaternion.Euler(thing.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, thing.eulerAngles.z);
        thing.rotation = LookAtRotationOnly_Y;
    }
}
