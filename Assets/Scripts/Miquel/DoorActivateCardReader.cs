using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivateCardReader : MonoBehaviour
{
    private CardPerms cardPerms;

    public void SetCardPerms(CardPerms perms)
    {
        cardPerms = perms;
    }

    public void ActivateCardReader()
    {
        CardReader.toggleSingleCardReaderRaycast.Invoke(cardPerms, true);
        transform.parent.GetComponent<DoorLab>().SetDoor(false);
    }
}
