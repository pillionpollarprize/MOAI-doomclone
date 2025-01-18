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
            case "HPSMAL":
                notifText.text = "Ate delicious meat from unknown origins.";
                break;
            case "REDKEY":
                notifText.text = "Picked up a red keycard.";
                break;
            case "BLUKEY":
                notifText.text = "Picked up a blue keycard.";
                break;
            case "GREKEY":
                notifText.text = "Picked up a green keycard.";
                break;
            case "RCKAMM":
                notifText.text = "Picked up rocks.";
                break;

        }
        Invoke("ResetText", 5);
    }
    public void ResetText()
    {
        notifText.text = "";
    }
}
