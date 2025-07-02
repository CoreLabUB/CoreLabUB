using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Bindings.Variables;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MicroscopeButton : RaycastInteractable
{
    [SerializeField] private DoorController microscopeDoor;
    [SerializeField] private MicroscopeDetection microscopeDetection;

    protected override void Awake()
    {
        base.Awake();

        GetComponent<XRSimpleInteractable>().activated.AddListener(_ =>
        {
            Activate(_.interactorObject.transform.gameObject);
            
        });

        GetComponent<XRSimpleInteractable>().deactivated.AddListener(_ =>
        {
            Deactivate(_.interactorObject.transform.gameObject);
            
        });
    }

    public override void Activate(GameObject interactor)
    {
        microscopeDoor.ToggleDoor();

        if (GetDoorOpen() == false)
        {
            microscopeDetection.Detect();
        }
    }

    public override void Deactivate(GameObject interactor)
    {
        Debug.Log("DEACTIVATE");
    }

    public bool GetDoorOpen()
    { return microscopeDoor.GetSemIsOpen(); }
}
