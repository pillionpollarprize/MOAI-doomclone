using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Keycard : MonoBehaviour
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
            arsenal.hasRKeycard = true;
            print("keycard acquired");
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