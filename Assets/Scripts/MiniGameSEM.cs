using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSEM : MonoBehaviour
{
    [SerializeField] private AudioClip errorWindows;
    [SerializeField] private AudioClip fallarImagen;
    [SerializeField] private AudioClip AcertarImagen;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject ImagenPollenPixel;
    [SerializeField] private GameObject ERROR;

    [SerializeField] private GameObject PollenCorrecto;
    [SerializeField] private GameObject PollenIncorrecto1;
    [SerializeField] private GameObject PollenIncorrecto2;
    [SerializeField] private GameObject PollenIncorrecto3;
    [SerializeField] private GameObject IMAGENFINAL;
    [SerializeField] private GameObject MASBOTONZOOM;
    [SerializeField] private GameObject MENOSBOTONZOOM;
    [SerializeField] private GameObject milAmuento;
    [SerializeField] private GameObject quinentosAmuento;
    [SerializeField] private GameObject unoAmuento;
    private int index = 0;

    private void Update()
    {
        if(index == 3)
        {
            milAmuento.SetActive(true);
            quinentosAmuento.SetActive(false);
            unoAmuento.SetActive(false);
        }
        else if(index == 2)
        {
            milAmuento.SetActive(false);
            quinentosAmuento.SetActive(true);
            unoAmuento.SetActive(false);
        }
        else if (index == 1)
        {
            milAmuento.SetActive(false);
            quinentosAmuento.SetActive(false);
            unoAmuento.SetActive(true);
        }
    }

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ComenzarMinijuego()
    {
        StartButton.SetActive(false);
        startGameButton.SetActive(true);
        ImagenPollenPixel.SetActive(true);
        ERROR.SetActive(true);
        audioSource.PlayOneShot(errorWindows);
    }

    public void ComenzarAdivinarIMGpixelada()
    {
        startGameButton.SetActive(false);
        ImagenPollenPixel.SetActive(false);
        ERROR.SetActive(false);

        PollenCorrecto.SetActive(true);
        PollenIncorrecto1.SetActive(true);
        PollenIncorrecto2.SetActive(true);
        PollenIncorrecto3.SetActive(true);
    }

    public void Fallo()
    {
        audioSource.PlayOneShot(fallarImagen);
    }


    public void Acierto()
    {
        audioSource.PlayOneShot(AcertarImagen);
        PollenCorrecto.SetActive(false);
        PollenIncorrecto1.SetActive(false);
        PollenIncorrecto2.SetActive(false);
        PollenIncorrecto3.SetActive(false);
        IMAGENFINAL.SetActive(true);
        MASBOTONZOOM.SetActive(true);
        MENOSBOTONZOOM.SetActive(true);
        index = 3;
    }


    public void SubirZOOM()
    {
        index++;
        if(index >= 4)
        {
            index = 3;
        }
    }


    public void BajaZOO()
    {
        index--;
        if(index <= 0)
        {
            index = 1;
        }
    }

}
