using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextAsset creditsNames;

    [SerializeField] string sceneToPlay;

    void Awake()
    {
        SetPlayButton();
        SetCredits();
    }

    private void SetCredits()
    {
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = creditsNames.text;
    }

    private void SetPlayButton()
    {
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = LocalizationManager.Localize("PlayButton");
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToPlay);
        });
    }
}
