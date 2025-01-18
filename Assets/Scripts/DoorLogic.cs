using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public Items items;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var arsenal = col.gameObject.GetComponent<Arsenal>();
            var notsys = col.gameObject.GetComponent<NotificationSys>();
            if (items == Items.RedKey && arsenal.hasRKeycard 
                || items == Items.BlueKey && arsenal.hasBKeycard
                || items == Items.GreenKey && arsenal.hasGKeycard)
            {
                Destroy(gameObject);
                print("door open :)");
            }
        }
        
    }
}