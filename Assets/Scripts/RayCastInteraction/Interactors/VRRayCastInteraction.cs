using System.Collections;
using UnityEngine;

public enum HandState { IDLE = 0, POINTING = 1, GRABBING = 2, INTERACT = 3 }

public class VRRaycastInteraction : BaseRaycastInteraction
{
    [Header("Variables")]
    [SerializeField] private float raycastInteractionLength = 2;
    [SerializeField] private LayerMask interactableLayerMask;

    [Header("Assign Objects")]
    // Hands' GameObjects
    [SerializeField] private GameObject right_controller;
    [SerializeField] private GameObject left_controller;

    // Interacted Objects
    private GameObject right_lastInteractedObject;
    private GameObject left_lastInteractedObject;

    // Right Hand Animation
    [SerializeField] private Animator right_handAnimator;
    private HandState right_handState = HandState.IDLE;
    private bool right_blockChangeAnimation = false;

    // Left Hand Animation
    [SerializeField] private Animator left_handAnimator;
    private HandState left_handState = HandState.IDLE;
    private bool left_blockChangeAnimation = false;

    // DragUpdate Wait
    private WaitForFixedUpdate right_waitForFixedUpdate = new WaitForFixedUpdate();
    private WaitForFixedUpdate left_waitForFixedUpdate = new WaitForFixedUpdate();

    
    private void Start()
    {
        #region RIGHT HAND

        // Hand Trigger

        playerInputs.XRIRightHandInteraction.Select.started += _ => { RightHand_HandTriggerDown(); };
        playerInputs.XRIRightHandInteraction.Select.canceled += _ => { RightHand_HandTriggerUp(); };

        // Index Trigger
        playerInputs.XRIRightHandInteraction.Activate.started += _ => { RightHand_IndexTriggerDown(); };
        playerInputs.XRIRightHandInteraction.Activate.canceled += _ => { RightHand_IndexTriggerUp(); };

        #endregion

        #region LEFT HAND

        // Hand Trigger
        playerInputs.XRILeftHandInteraction.Select.started += _ => { LeftHand_HandTriggerDown(); };
        playerInputs.XRILeftHandInteraction.Select.canceled += _ => { LeftHand_HandTriggerUp(); };

        // Index Trigger
        playerInputs.XRILeftHandInteraction.Activate.started += _ => { LeftHand_IndexTriggerDown(); };
        playerInputs.XRILeftHandInteraction.Activate.canceled += _ => { LeftHand_IndexTriggerUp(); };


        //InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        //InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
        #endregion
    }

    private void Update()
    {
        Right_HoverUpdate();
        Left_HoverUpdate();
    }

    #region RIGHT HAND
    // These functions are the interactions with the two main buttons of the controller. FOR THE RIGHT HAND
    protected virtual void RightHand_HandTriggerDown()
    {
        Ray ray = new Ray(right_controller.transform.position, right_controller.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastInteractionLength, interactableLayerMask))
        {
            right_lastInteractedObject = hit.transform.gameObject;

            right_lastInteractedObject.GetComponent<RaycastInteractable>().Grab();
            

            StartCoroutine(Right_DragUpdate(right_lastInteractedObject));
            Right_ChangeHandState(HandState.GRABBING, right_blockChangeAnimation);
            right_blockChangeAnimation = true;
        }
        else
        {
            Right_ChangeHandState(HandState.IDLE, right_blockChangeAnimation);
        }
    }
    protected virtual void RightHand_HandTriggerUp()
    {
        if (right_lastInteractedObject == null)
        { return; } 

        right_blockChangeAnimation = false;
        Right_ChangeHandState(HandState.POINTING, right_blockChangeAnimation);

        right_lastInteractedObject.GetComponent<RaycastInteractable>().Cancel();
        
        right_lastInteractedObject = null;
    }

    protected virtual void RightHand_IndexTriggerDown()
    {
        Ray ray = new Ray(right_controller.transform.position, right_controller.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastInteractionLength, interactableLayerMask))
        {
            right_lastInteractedObject = hit.transform.gameObject;

            right_lastInteractedObject.GetComponent<RaycastInteractable>().Interact();

            Right_ChangeHandState(HandState.INTERACT, right_blockChangeAnimation);
            right_blockChangeAnimation = true;
        }
        else
        {
            Right_ChangeHandState(HandState.IDLE, right_blockChangeAnimation);
        }
    }

    protected virtual void RightHand_IndexTriggerUp()
    {
        if (right_lastInteractedObject == null)
        { return; }

        right_blockChangeAnimation = false;
        Right_ChangeHandState(HandState.POINTING, right_blockChangeAnimation);

        right_lastInteractedObject.GetComponent<RaycastInteractable>().Cancel();

        right_lastInteractedObject = null;
    }
    #endregion

    #region LEFT HAND
    // These functions are the interactions with the two main buttons of the controller. FOR THE LEFT HAND
    protected virtual void LeftHand_HandTriggerDown()
    {
        Ray ray = new Ray(left_controller.transform.position, left_controller.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastInteractionLength, interactableLayerMask))
        {
            left_lastInteractedObject = hit.transform.gameObject;

            left_lastInteractedObject.GetComponent<RaycastInteractable>().Grab();

            StartCoroutine(Left_DragUpdate(left_lastInteractedObject));
            Left_ChangeHandState(HandState.GRABBING, left_blockChangeAnimation);
            left_blockChangeAnimation = true;
        }
        else
        {
            Left_ChangeHandState(HandState.IDLE, left_blockChangeAnimation);
        }
    }
    protected virtual void LeftHand_HandTriggerUp()
    {
        if (left_lastInteractedObject == null)
        { return; }

        left_blockChangeAnimation = false;
        Left_ChangeHandState(HandState.POINTING, left_blockChangeAnimation);

        left_lastInteractedObject.GetComponent <RaycastInteractable>().Cancel();

        left_lastInteractedObject = null;
    }

    protected virtual void LeftHand_IndexTriggerDown()
    {
        Ray ray = new Ray(left_controller.transform.position, left_controller.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastInteractionLength, interactableLayerMask))
        {
            left_lastInteractedObject = hit.transform.gameObject;

            left_lastInteractedObject.GetComponent<RaycastInteractable>().Interact();

            Left_ChangeHandState(HandState.INTERACT, left_blockChangeAnimation);
            left_blockChangeAnimation = true;
        }
        else
        {
            Left_ChangeHandState(HandState.POINTING, left_blockChangeAnimation);
        }
    }

    protected virtual void LeftHand_IndexTriggerUp()
    {
        if (left_lastInteractedObject == null)
        { return; }

        left_blockChangeAnimation = false;
        Left_ChangeHandState(HandState.POINTING, left_blockChangeAnimation);

        left_lastInteractedObject.GetComponent<RaycastInteractable>().Cancel();

        left_lastInteractedObject = null;
    }
    #endregion


    #region DRAG UPDATE
    // These Functions are active when an object with the correct layermask is selected and the Hand Trigger is down

    public virtual IEnumerator Right_DragUpdate(GameObject clickedGameObject)
    {
        while (playerInputs.XRIRightHandInteraction.Select.ReadValue<float>() != 0)
        {
            // Do Something to Object while dragging
            yield return right_waitForFixedUpdate;
        }
    }

    public virtual IEnumerator Left_DragUpdate(GameObject clickedGameObject)
    {
        while (playerInputs.XRILeftHandInteraction.Select.ReadValue<float>() != 0)
        {
            // Do Something to Object while dragging
            yield return left_waitForFixedUpdate;
        }
    }
    #endregion

    #region HANDS HOVER UPDATE
    // These Functions are constantly updating, they are in Update(), and change the hand state to idle
    // Can be used for example to add a highlighting to the object hit

    public virtual void Right_HoverUpdate()
    {
        Ray ray = new Ray(right_controller.transform.position, right_controller.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastInteractionLength, interactableLayerMask))
        {
            Right_ChangeHandState(HandState.POINTING, right_blockChangeAnimation);
        }
        else
        {
            Right_ChangeHandState(HandState.IDLE, right_blockChangeAnimation);
        }
    }

    public virtual void Left_HoverUpdate()
    {
        Ray ray = new Ray(left_controller.transform.position, left_controller.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastInteractionLength, interactableLayerMask))
        {
            Left_ChangeHandState(HandState.POINTING, left_blockChangeAnimation);
        }
        else
        {
            Left_ChangeHandState(HandState.IDLE, left_blockChangeAnimation);
        }
    }
    #endregion

    #region HAND ANIMATION

    // These functions change the hand state
    // There are some optimizations done such as if there has been no change to the hand state or its blocked, the function ends
    protected virtual void Right_ChangeHandState(HandState newHandState, bool blockAnimation)
    {
        if (right_handState == newHandState || blockAnimation) { return; }

        switch (newHandState)
        {
            case HandState.IDLE:
            {
                right_handAnimator.SetBool("RightIndexPoint", false);
                right_handAnimator.SetBool("RightIndexInteract", false);
                right_handAnimator.SetBool("RightHandGrab", false);
                right_handState = newHandState;
                break;
            }
            case HandState.POINTING:
            {
                right_handAnimator.SetBool("RightIndexPoint", true);
                right_handAnimator.SetBool("RightIndexInteract", false);
                right_handAnimator.SetBool("RightHandGrab", false);
                right_handState = newHandState;
                break;
            }
            case HandState.GRABBING:
            {

                right_handAnimator.SetBool("RightIndexPoint", true);
                right_handAnimator.SetBool("RightIndexInteract", false);
                right_handAnimator.SetBool("RightHandGrab", true);
                right_handState = newHandState;
                break;
            }
            case HandState.INTERACT:
            {
                right_handAnimator.SetBool("RightIndexPoint", true);
                right_handAnimator.SetBool("RightIndexInteract", true);
                right_handAnimator.SetBool("RightHandGrab", false);
                right_handState = newHandState;
                break;
            }
        }
        
    }

    protected virtual void Left_ChangeHandState(HandState newHandState, bool blockAnimation)
    {
        if (left_handState == newHandState || blockAnimation) { return; }

        switch (newHandState)
        {
            case HandState.IDLE:
                {
                    left_handAnimator.SetBool("LeftIndexPoint", false);
                    left_handAnimator.SetBool("LeftIndexInteract", false);
                    left_handAnimator.SetBool("LeftHandGrab", false);
                    left_handState = newHandState;
                    break;
                }
            case HandState.POINTING:
                {
                    left_handAnimator.SetBool("LeftIndexPoint", true);
                    left_handAnimator.SetBool("LeftIndexInteract", false);
                    left_handAnimator.SetBool("LeftHandGrab", false);
                    left_handState = newHandState;
                    break;
                }
            case HandState.GRABBING:
                {

                    left_handAnimator.SetBool("LeftIndexPoint", true);
                    left_handAnimator.SetBool("LeftIndexInteract", false);
                    left_handAnimator.SetBool("LeftHandGrab", true);
                    left_handState = newHandState;
                    break;
                }
            case HandState.INTERACT:
                {
                    left_handAnimator.SetBool("LeftIndexPoint", true);
                    left_handAnimator.SetBool("LeftIndexInteract", true);
                    left_handAnimator.SetBool("LeftHandGrab", false);
                    left_handState = newHandState;
                    break;
                }
        }
    }
    #endregion
}
