using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeButton : RaycastInteractable
{
    [SerializeField] private DoorController microscopeDoor;
    [SerializeField] private MicroscopeDetection microscopeDetection;
    public override void Interact()
    {
        microscopeDoor.ToggleDoor();

        if (GetDoorOpen() == false)
        {
            microscopeDetection.Detect();
        }
    }

    public override void Cancel()
    {
        
    }

    public bool GetDoorOpen()
    { return microscopeDoor.GetSemIsOpen(); }
}
