using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public enum Language { Catalan, Spanish, English }

public class PopupsManager : Singleton<PopupsManager>
{
    [SerializeField] Language activeLanguage;

    private int currentPopupId = 0;
    private TMP_Text popupText;

    Dictionary<int, string> localizationKeys = new();

    void Start()
    {
        LocalizationManager.Read();
        LocalizationManager.Language = activeLanguage.ToString();

        var temp = LocalizationManager.Dictionary;
        int count = 0;
        foreach ( var key in temp.Values )
        {
            foreach (var keys in key.Keys)
            {
                localizationKeys.Add(count, keys);
                count++;
            }
        }

        popupText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    public void CreatePopup(int id, Vector3 pos, Quaternion rot)
    {
        Debug.Log("CREATE | CURRENT ID: " + currentPopupId + " ID PARAMETER: " + id);

        if (currentPopupId != id) { return; }
       
        popupText.transform.parent.gameObject.SetActive(true);
        popupText.text = LocalizationManager.Localize(localizationKeys[id]);
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
