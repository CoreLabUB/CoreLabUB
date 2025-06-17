using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Transform tr;

    private bool rotate_door;

    [SerializeField] private Animator anim;

    public int dir;

    [SerializeField] private GameObject fade_ob;
    private bool fade;

    private AudioSource audioSource;
    public static GameManager Instance { private set; get; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning(this.gameObject.name + " had a GameManager script.");
            Destroy(this);
        }
    }


    public void LoadScene(string sceneName)
    {
           SceneManager.LoadScene(sceneName);
    }



}