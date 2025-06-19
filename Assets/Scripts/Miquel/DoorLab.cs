using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorLab : MonoBehaviour
{
    // Which Lab Door it is
    private CardPerms labPerm;

    [SerializeField] private Animator leftDoorAnimator;
    [SerializeField] private Animator rightDoorAnimator;

    private void Awake()
    {
        leftDoorAnimator.SetBool("isLeftDoor", true);
        rightDoorAnimator.SetBool("isLeftDoor", false);


        // After Animation, a Animation Event is triggered calling "toggleSingleCardReaderRaycast" CardReader event
        // This is setting the correct CardReader
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(0).GetComponent<DoorActivateCardReader>().SetCardPerms(labPerm);
        }
    }

    public void SetDoor(bool state)
    {
        leftDoorAnimator.SetBool("StartDoorAnimation", state);
        rightDoorAnimator.SetBool("StartDoorAnimation", state);
    }

    public void SetCardPerms(CardPerms perm)
    { labPerm = perm; }
}
