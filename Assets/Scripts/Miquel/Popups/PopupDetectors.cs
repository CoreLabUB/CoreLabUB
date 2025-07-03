using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDetectors : PopupsRaycast
{
    void Update()
    {
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, rayDistance, interactableLayerMask))
        { return; }

        Transform child = transform.GetChild(0);

        PopupsManager.Instance.CreatePopup(popupId, child.position, child.rotation);


        Destroy(gameObject);
    }
}
