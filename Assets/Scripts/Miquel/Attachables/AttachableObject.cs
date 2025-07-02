using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachableObject : RaycastInteractable
{
    private Transform originPos;
    private GameObject attachTransform;

    // Cancel() is called first then OnAttach, so this variable assures there is no teleportation
    bool lockAttached = false;

    protected bool isHovering = false;
    protected bool isAttatch = false;

    private void Start()
    {
        attachTransform = transform.GetChild(0).gameObject;
        originPos = attachTransform.transform;
    }

    public override void HoverEnter(GameObject interactor)
    {
        base.HoverEnter(interactor);
        isHovering = true;
    }

    public override void HoverExit(GameObject interactor)
    {
        base.HoverExit(interactor);
        isHovering = false;
    }

    public virtual void OnAttach(Transform attachPosition)
    {
        lockAttached = true;

        GetComponent<XRGrabInteractable>().movementType = XRBaseInteractable.MovementType.Instantaneous;
        GetComponent<XRGrabInteractable>().smoothPosition = false;

        isAttatch = true;
        Debug.Log(originPos + " "  + attachPosition);
        originPos = attachPosition;

        ReturnToPreviousPosition();

        lockAttached = false;
    }

    public virtual void OnDisattach()
    {
        GetComponent<XRGrabInteractable>().movementType = XRBaseInteractable.MovementType.VelocityTracking;
        GetComponent<XRGrabInteractable>().smoothPosition = true;

        isAttatch = false;
    }

    public override void SelectExit(GameObject interactor)
    {
        if (lockAttached == false) { return; }

        ReturnToPreviousPosition();
    }

    protected void ReturnToPreviousPosition()
    {
        Debug.Log("RETURN TO: " + originPos);
        transform.position = originPos.position;
    }
}
