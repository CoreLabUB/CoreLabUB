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
        if (hasSubstance) { return; }

        if (emitter.GetComponent<Stick>().GetState() == StickState.GetSample) { return; }

        InsertSubstance(emitter.GetComponent<Stick>().GetSubstanceMaterial());
        hasSubstance = true;

        
        Destroy(emitter);
    }
}
