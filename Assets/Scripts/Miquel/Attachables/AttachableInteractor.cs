using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableInteractor : MonoBehaviour
{
    private bool isOccupied;
    private AttachableObject attachableObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AttachObject(AttachableObject attachObject)
    {
        if (isOccupied) { return false; }

        isOccupied = true;
        attachableObject = attachObject;

        return true;
    }

    public void DisattachObject()
    {
        isOccupied = false;
        attachableObject = null;
    }

    public bool IsOccupied() { return isOccupied; }
}
