using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : BaseSubstance
{
    protected override void Awake()
    {
        //GetComponent<Renderer>().material = substanceMaterial;
        substanceType = SubstanceType.Grass;
    }

    void Update()
    {
        
    }
}
