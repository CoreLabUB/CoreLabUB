using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : BaseSubstance
{
    protected override void Awake()
    {
        base.Awake();
        substanceType = SubstanceType.Pollen;
    }

    void Update()
    {
        
    }
}
