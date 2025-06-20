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
        Debug.Log("PALO INTERACT");
    }
    public virtual void Grab() // Hand Trigger Down
    {
        Debug.Log("PALO GRAB");
    }

    public virtual void Drag()
    {
        if (!canDrag) { return; }
    }

    public virtual void Cancel() // Trigger Up
    {
        Debug.Log("PALO CANCEL");
    }
    public InteractableType GetInteractableType()
    {
        return interactableType;
    }
}
