using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivateCardReader : MonoBehaviour
{
    // This class is for when animation ends to Trigger ActivateCardReader

    private CardPerms cardPerms;

    public void SetCardPerms(CardPerms perms)
    {
        cardPerms = perms;
    }

    public void ActivateCardReader()
    {
        CardReader.toggleCardReaderRaycast.Invoke(true);
        transform.parent.GetComponent<DoorLab>().SetDoor(false);
    }
}
