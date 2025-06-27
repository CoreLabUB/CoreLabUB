using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttachableObject : RaycastInteractable
{
    private Vector3 originPos;
    private GameObject attachTransform;

    // Cancel() is called first then OnAttach, so this variable assures there is no teleportation
    bool lockAttached = false;

    protected bool isHovering = false;
    protected bool isAttatch = false;

    private void Start()
    {
        attachTransform = transform.GetChild(0).gameObject;
        originPos = attachTransform.transform.position;
    }

    public virtual void OnHoverEnter()
    {
        isHovering = true;
    }

    public virtual void OnHoverExit()
    {
        isHovering = false;
    }

    public virtual void OnAttach(Vector3 attachPosition)
    {
        lockAttached = true;

        isAttatch = true;
        Debug.Log(originPos + " "  + attachPosition);
        originPos = attachPosition;

        ReturnToPreviousPosition();

        lockAttached = false;
    }

    public virtual void OnDisattach()
    {
        isAttatch = false;
    }

    public override void Cancel()
    {
        if (lockAttached == false) { return; }

        ReturnToPreviousPosition();
    }

    protected void ReturnToPreviousPosition()
    {
        Debug.Log("RETURN TO: " + originPos);
        transform.position = originPos;
    }
}
