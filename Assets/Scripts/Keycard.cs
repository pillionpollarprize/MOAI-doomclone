using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public Type type;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var arsenal = col.gameObject.GetComponent<Arsenal>();
            var notsys = col.gameObject.GetComponent<NotificationSys>();
            arsenal.hasRKeycard = true;
            notsys.SetNotifText("REDKEY");
        }
        Destroy(gameObject);
    }
}
public enum Type
{
    Red,
    Blue,
    Green
}