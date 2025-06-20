using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioSource : CustomAudioSource
{
    // Timer to delete this GameObject
    private float count = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (audioClip == null)
        { return; }

        DestroyWhenClipFinishes();
    }

    private void DestroyWhenClipFinishes()
    {
        count += Time.deltaTime;
        if (count < audioClip.length)
        { return; }

        Destroy(gameObject);
    }
}
