using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationSys : MonoBehaviour
{
    public TextMeshProUGUI notifText ;
    void Start()
    {
        notifText.text = "";
    }

    public void SetNotifText(string type)
    {
        switch (type)
        {
            case "REDKEY":
                notifText.text = "You picked up a red keycard.";
                break;
        }
        Invoke("ResetText", 5);
    }
    public void ResetText()
    {
        notifText.text = "";
    }
}
