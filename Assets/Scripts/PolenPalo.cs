using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolenPalo : MonoBehaviour
{
    private bool tienePolen = false;
    [SerializeField] private GameObject sinPolen;
    [SerializeField] private GameObject conPolen;


    private void Update()
    {
        if (tienePolen)
        {
            sinPolen.SetActive(false);
            conPolen.SetActive(true);
        }
        else
        {

            sinPolen.SetActive(true);
            conPolen.SetActive(false);
        }

    }

    public void SetPolen(bool newStatus)
    {
        tienePolen = newStatus;
    }

    public bool GetPolen()
    {
        return tienePolen;
    }
}
