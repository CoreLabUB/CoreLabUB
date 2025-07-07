using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguage : MonoBehaviour
{
    [SerializeField] private GameObject languagePrefab;

    private void Awake()
    {
        LocalizationManager.Read();
        LocalizationManager.AutoLanguage();

        var temp = LocalizationManager.Dictionary;

        transform.GetChild(0).GetComponent<TMP_Text>().text = LocalizationManager.Localize("Instruction");

        foreach (var language in temp.Keys)
        {
            Debug.Log(language);
            GameObject prefab = Instantiate(languagePrefab, transform.GetChild(1));
            prefab.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Flags/"+language+"Flag");
            prefab.transform.GetChild(1).GetComponent<TMP_Text>().text = LocalizationManager.Localize(String.Concat("LanguageNames",language));

            prefab.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => 
            {
                LocalizationManager.Language = language; 
                transform.parent.GetChild(1).gameObject.SetActive(true);  
                gameObject.SetActive(false); 
            });
        }
    }
}
