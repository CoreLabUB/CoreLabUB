using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject Door1;
    [SerializeField] private GameObject Door;
    private BoxCollider bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
      if(other.tag == "Open")
        {
            Debug.Log("aaa");
        }
    }
}
