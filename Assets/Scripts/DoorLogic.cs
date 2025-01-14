using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public Type type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var arsenal = col.gameObject.GetComponent<Arsenal>();
            if (arsenal.hasRKeycard)
            {
                Destroy(gameObject);
                print("door open :)");
            }
            else
            {
                print("door no open >:(");
            }
        }
        
    }
}