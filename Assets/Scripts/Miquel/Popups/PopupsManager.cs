using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PopupsManager : Singleton<PopupsManager>
{
    private int currentPopupId = 0;
    private TMP_Text popupText;

    Dictionary<int, string> popups = new()
    {
        {0, "GRAB A CARD\nAND GO TO ARCHEOLAB"},
        {1, "OVER HERE"},
    };

    void Start()
    {
        popupText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    public void CreatePopup(int id, Vector3 pos, Quaternion rot)
    {
        Debug.Log("CREATE | CURRENT ID: " + currentPopupId + " ID PARAMETER: " + id);

        if (currentPopupId != id) { return; }
       
        popupText.transform.parent.gameObject.SetActive(true);
        popupText.text = popups[currentPopupId];
        popupText.transform.parent.parent.SetPositionAndRotation(pos, rot);

        
    }

    public void ClosePopup(int id)
    {
        Debug.Log("CLOSE | CURRENT ID: " + currentPopupId + " ID PARAMETER: " + id);

        if (currentPopupId != id) { return;}

        popupText.transform.parent.gameObject.SetActive(false);

        currentPopupId++;
    }
}
