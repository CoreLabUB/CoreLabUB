using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioSource : MonoBehaviour
{
    // Clip to be played
    private AudioClip audioClip;

    // Timer to delete this GameObject
    private float count;

    private void Update()
    {
        if (audioClip == null) 
        { return; }

        DestroyWhenClipFinishes();
    }

    public void PlayClip(AudioClip clip)
    {
        audioClip = clip;
        GetComponent<AudioSource>().clip = audioClip;

        GetComponent<AudioSource>().Play();
    }

    private void DestroyWhenClipFinishes()
    {
        count += Time.deltaTime;
        if (count < audioClip.length) 
        { return; }

        Destroy(gameObject);
    }
}
