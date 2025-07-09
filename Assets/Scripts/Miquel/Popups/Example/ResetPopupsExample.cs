using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPopupsExample : MonoBehaviour
{
    [SerializeField] List<GameObject> popupsRaycasts;

    void Start()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            popupsRaycasts.ForEach(_ => { _.SetActive(true); });
        });
    }
}
