using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    private Arsenal arsenal;
    public GameObject keyObj;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        arsenal = FindObjectOfType<PlayerMove>().GetComponent<Arsenal>();
    }
    void Update()
    {
        if (arsenal.hasRKeycard)
        {
            keyObj.SetActive(true);
        }
    }
}
