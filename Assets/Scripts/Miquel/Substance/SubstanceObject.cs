using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceObject : RaycastTarget
{
    public override void OnRaycastEnter(GameObject emitter)
    {
        if (emitter.GetComponent<Stick>().GetState() == StickState.GetSample)
            emitter.GetComponent<Stick>().GetHeadAudioSource().UnPause();
    }

    public override void OnRaycastExit(GameObject emitter)
    {
        if (emitter.GetComponent<Stick>().GetState() == StickState.GetSample)
            emitter.GetComponent<Stick>().GetHeadAudioSource().Pause();
    }
}
