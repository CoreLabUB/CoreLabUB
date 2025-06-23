using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceSample : RaycastTarget
{
    bool hasSubstance = false;
    GameObject sample;

    protected override void Awake()
    {
        base.Awake();
        sample = transform.GetChild(0).GetChild(0).gameObject;
    }

    public void InsertSubstance(Material sampleMaterial)
    {
        sample.SetActive(true);
        sample.GetComponent<Renderer>().material = sampleMaterial;
    }

    public override void OnRaycastEnter(GameObject emitter)
    {
        Debug.Log("ENTERING SAMPLE " + emitter.GetComponent<Stick>().GetState().ToString());
        if (hasSubstance || emitter.GetComponent<Stick>().GetState() == StickState.GetSample) { return; }
        Debug.Log("ENTERED SAMPLE");
        InsertSubstance(emitter.GetComponent<Stick>().GetSubstanceMaterial());
        hasSubstance = true;

        emitter.GetComponent<Stick>().Cancel();
        Destroy(emitter);
    }
}
