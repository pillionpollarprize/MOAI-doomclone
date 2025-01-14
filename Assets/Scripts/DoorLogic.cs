using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public Type type;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var arsenal = col.gameObject.GetComponent<Arsenal>();
            var notsys = col.gameObject.GetComponent<NotificationSys>();
            if (type == Type.Red && arsenal.hasRKeycard 
                || type == Type.Blue && arsenal.hasBKeycard
                || type == Type.Green && arsenal.hasGKeycard)
            {
                Destroy(gameObject);
                print("door open :)");
            }
        }
        
    }
}