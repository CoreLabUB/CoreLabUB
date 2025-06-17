using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegoerPolen : MonoBehaviour
{
    private BoxCollider bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Palo")
        {
            GameObject temp = other.gameObject;

            PolenPalo polenRef = temp.GetComponent<PolenPalo>();

            polenRef.SetPolen(true);
        }
    }
}
