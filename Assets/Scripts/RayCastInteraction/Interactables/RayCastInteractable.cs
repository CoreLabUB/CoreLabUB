using UnityEngine;


/* ---------------------------------
    MADE BY MIQUEL FORCADA MERCADE
    https://www.linkedin.com/in/miquel-forcada-mercade/
*/

public enum InteractableType { NULL, TEST }
public class RaycastInteractable : MonoBehaviour
{
    protected InteractableType interactableType = InteractableType.NULL;

    private void Awake()
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

    public virtual void Cancel() // Trigger Up
    {
        Debug.Log("PALO CANCEL");
    }
    public InteractableType GetInteractableType()
    {
        return interactableType;
    }
}
