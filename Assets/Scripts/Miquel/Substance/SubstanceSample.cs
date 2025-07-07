using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceSample : RaycastTarget
{
    bool hasSubstance = false;
    GameObject sample;
    SubstanceType substanceType;

    protected override void Awake()
    {
        base.Awake();
        sample = transform.GetChild(0).GetChild(0).gameObject;
    }

    public void InsertSubstance(Material sampleMaterial, SubstanceType substance)
    {
        sample.SetActive(true);
        sample.GetComponent<Renderer>().material = sampleMaterial;
        substanceType = substance;
    }

    public override void OnRaycastEnter(GameObject emitter)
    {
        if (hasSubstance) { return; }

        // Returns if Stick doesnot have a sample
        if (emitter.GetComponent<Stick>().GetState() == StickState.GetSample) { return; }

        InsertSubstance(emitter.GetComponent<Stick>().GetSubstanceMaterial(), emitter.GetComponent<Stick>().GetSubstance().GetSubstanceType());
        hasSubstance = true;
        
        Destroy(emitter);
    }

    public SubstanceType GetSubstanceType() 
    { return substanceType; }
}
