using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

public class ChangeLanguageExample : MonoBehaviour
{
    [SerializeField] GameObject languagePrefab;
    List<string> languages = new List<string>();
    void Start()
    {
        LocalizationManager.Read();

        var temp = LocalizationManager.Dictionary;

        foreach (var language in temp.Keys)
        {
            languages.Add(language);
            GameObject prefab = Instantiate(languagePrefab, transform.GetChild(1));

            prefab.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Flags/" + language + "Flag");

            prefab.transform.GetChild(1).GetComponent<TMP_Text>().text = LocalizationManager.Localize(String.Concat("LanguageNames", language));

            // Button Onclick, Activate MainMenu and disable Language Selection
            prefab.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                LocalizationManager.Language = language;

                Debug.Log(language);
            });
        }

        LocalizationManager.OnLocalizationChanged += () =>
        {
            for (int i = 0; i < transform.GetChild(1).childCount; i++)
            {
                transform.GetChild(1).GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = LocalizationManager.Localize(String.Concat("LanguageNames", languages[i]));
            }
        };
    }
}
