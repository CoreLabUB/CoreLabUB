using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : BaseSubstance
{
    protected override void Awake()
    {
        //GetComponent<Renderer>().material = substanceMaterial;
        substanceType = SubstanceType.Pollen;
    }

    void Update()
    {
        
    }
}
