using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsClosers : PopupsRaycast
{
    void Update()
    {
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, rayDistance, interactableLayerMask))
        { return; }

        PopupsManager.Instance.ClosePopup(popupId);


        Destroy(gameObject);
    }
}
