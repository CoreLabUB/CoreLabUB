using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositarMuestra : MonoBehaviour
{
    private BoxCollider bc;
    [SerializeField] private GameObject muesta;
    public bool tieneMuestra = false;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Palo")
        {
            GameObject temp = other.gameObject;

            PolenPalo polenRef = temp.GetComponent<PolenPalo>();

            if (polenRef.GetPolen())
            {
                muesta.SetActive(true);
                tieneMuestra = true;
            }
        }
    }

    public bool GetTieneMuestra()
    {
        return tieneMuestra;
    }
}
