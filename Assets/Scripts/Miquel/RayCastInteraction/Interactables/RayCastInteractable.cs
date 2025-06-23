using UnityEngine;

public enum InteractableType { NULL, MagneticCard, Stick }
public class RaycastInteractable : MonoBehaviour
{
    protected InteractableType interactableType = InteractableType.NULL;
    
    protected bool canDrag = true;

    protected virtual void Awake()
    {
        gameObject.layer = 9; // RaycastInteractable
    }

    public virtual void Interact() // Index Trigger Down
    {
        Debug.Log("INTERACT");
    }
    public virtual void Grab() // Hand Trigger Down
    {
        Debug.Log("GRAB");
    }

    public virtual void Drag()
    {
        if (!canDrag) { return; }
        Debug.Log("DRAG");
    }

    public virtual void Cancel() // Trigger Up
    {
        Debug.Log("CANCEL");
    }

    public InteractableType GetInteractableType()
    {
        return interactableType;
    }
}
