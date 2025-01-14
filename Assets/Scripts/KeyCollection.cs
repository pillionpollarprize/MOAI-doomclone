using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public GameObject keyObj;

    private Arsenal arsenal;
    private void Start()
    {
        arsenal = gameObject.GetComponent<Arsenal>();
    }
    void Update()
    {
        if (arsenal.hasRKeycard)
        {
            keyObj.SetActive(true);
        }
    }
}
