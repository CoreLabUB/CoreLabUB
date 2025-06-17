using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobarMuestra : MonoBehaviour
{
    private BoxCollider bc;
    [SerializeField] private DoorController door;
    [SerializeField]private bool isMustra = false;

    [SerializeField] private GameObject polen;
    private void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!door.GetSemIsOpen() && isMustra && polen != null)
        {
            polen.SetActive(true);
            polen = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DepositarMuestra temp = other.GetComponent<DepositarMuestra>();

        if (other.tag == "Muestra" && temp.tieneMuestra)
        {
            isMustra = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Muestra")
        {
            isMustra = false;
        }
    }
}
