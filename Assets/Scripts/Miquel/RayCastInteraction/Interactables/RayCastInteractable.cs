using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum InteractableType { NULL, MagneticCard, Stick }
public class RaycastInteractable : MonoBehaviour
{
    protected InteractableType interactableType = InteractableType.NULL;
    
    protected bool canDrag = true;
    protected bool isDragging = true;

    protected virtual void Awake()
    {
        gameObject.layer = 9; // RaycastInteractable
    }

    public virtual void Activate(GameObject interactor) // BOTH Trigger Down
    {
        ChangeHandState(interactor, HandState.INTERACT, false);
    }

    public virtual void Deactivate(GameObject interactor) // BOTH Trigger Up
    {
        ChangeHandState(interactor, HandState.IDLE, false);
    }

    public virtual void HoverEnter(GameObject interactor)
    {
        ChangeHandState(interactor, HandState.POINTING, false);
    }

    public virtual void HoverExit(GameObject interactor)
    {
        ChangeHandState(interactor, HandState.IDLE, false);
    }

    public virtual void SelectEnter(GameObject interactor) // Hand Trigger Down
    {
        if (canDrag) { isDragging = true; }

        interactor.transform.parent.GetChild(4).gameObject.SetActive(false);
        interactor.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    public virtual IEnumerator Grab()
    {
        yield return null;
    }

    public virtual void SelectExit(GameObject interactor)
    {
        if (canDrag) { isDragging = false; }

        interactor.transform.parent.GetChild(4).gameObject.SetActive(true);
        interactor.GetComponent<XRInteractorLineVisual>().enabled = true;
    }

    public virtual void Cancel() 
    {
        
    }

    public void ChangeHandState(GameObject interactor, HandState state, bool blockAnimation)
    {
        Debug.Log(interactor.name);
        interactor.transform.parent.GetComponent<Hand>().ChangeHandState(state, blockAnimation);
    }

    public InteractableType GetInteractableType()
    {
        return interactableType;
    }
}
