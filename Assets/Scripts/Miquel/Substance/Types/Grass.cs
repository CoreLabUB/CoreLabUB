using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : BaseSubstance
{
    protected override void Awake()
    {
        substanceType = SubstanceType.Grass;
    }

    void Update()
    {
        
    }
}
