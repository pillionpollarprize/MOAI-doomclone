using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public Items items;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && col.GetType().ToString().Equals("UnityEngine.CapsuleCollider"))
        {
            var arsenal = col.gameObject.GetComponent<Arsenal>();
            var notsys = col.gameObject.GetComponent<NotificationSys>();
            var health = col.gameObject.GetComponent<Health>();
            switch (items)
            {
                case Items.healthSmall:
                    health.TakeDamage(-10);
                    notsys.SetNotifText("HPSMAL");
                    break;
                case Items.rockAmmo:
                    arsenal.GetAmmo(2, 5);
                    notsys.SetNotifText("RCKAMM");
                    break;
                case Items.RedKey:
                    arsenal.hasRKeycard = true;
                    notsys.SetNotifText("REDKEY");
                    break;
                case Items.BlueKey:
                    arsenal.hasBKeycard = true;
                    notsys.SetNotifText("BLUKEY");
                    break;
                case Items.GreenKey:
                    arsenal.hasGKeycard = true;
                    notsys.SetNotifText("GREKEY");
                    break;
            }
            print("picked up");
            Destroy(gameObject);
        }
        
    }
}
public enum Items
{
    RedKey,
    BlueKey,
    GreenKey,
    healthSmall,
    healthMedium,
    healthLarge,
    rockAmmo,
}