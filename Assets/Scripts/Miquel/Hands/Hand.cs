using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public enum HandState { IDLE = 0, POINTING = 1, GRABBING = 2, INTERACT = 3 }
public class Hand : MonoBehaviour
{
    HandState currentHandState;
    [SerializeField] Animator handAnimation;
    [SerializeField] string handName;


    public virtual void ChangeHandState(HandState newHandState, bool blockAnimation)
    {
        if (currentHandState == newHandState || blockAnimation) { return; }

        switch (newHandState)
        {
            case HandState.IDLE:
            {
                handAnimation.SetBool(handName + "IndexPoint", false);
                handAnimation.SetBool(handName + "IndexInteract", false);
                handAnimation.SetBool(handName + "HandGrab", false);
                break;
            }
            case HandState.POINTING:
            {
                handAnimation.SetBool(handName + "IndexPoint", true);
                handAnimation.SetBool(handName + "IndexInteract", false);
                handAnimation.SetBool(handName + "HandGrab", false);
                break;
            }
            case HandState.GRABBING:
            {

                handAnimation.SetBool(handName + "IndexPoint", true);
                handAnimation.SetBool(handName + "IndexInteract", false);
                handAnimation.SetBool(handName + "HandGrab", true);
                break;
            }
            case HandState.INTERACT:
            {
                handAnimation.SetBool(handName + "IndexPoint", true);
                handAnimation.SetBool(handName + "IndexInteract", true);
                handAnimation.SetBool(handName + "HandGrab", false);
                break;
            }
        }
        currentHandState = newHandState;

    }
}
