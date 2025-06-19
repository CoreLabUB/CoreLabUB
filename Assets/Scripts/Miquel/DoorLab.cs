using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLab : MonoBehaviour
{
    // Which Lab Door it is
    private CardPerms labPerm;

    public void OpenDoor()
    {



        CardReader.toggleSingleCardReaderRaycast.Invoke(labPerm, true);
    }

    public void SetPerm(CardPerms perm)
    {  labPerm = perm; }
}
