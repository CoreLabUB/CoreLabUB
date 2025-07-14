using UnityEngine;

public class SubstanceSample : RaycastTarget
{
    private bool hasSubstance = false;
    private SubstanceType substanceType;

    protected override void Awake()
    {
        base.Awake();
    }

    private void InsertSubstance(Material sampleMaterial, SubstanceType substance)
    {
        GameObject sample = transform.GetChild(0).gameObject;
        for (int i = 0; i<3; i++)
        {
            sample.transform.GetChild(i).gameObject.SetActive(true);
            sample.transform.GetChild(i).GetComponent<Renderer>().material = sampleMaterial;
        }
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
