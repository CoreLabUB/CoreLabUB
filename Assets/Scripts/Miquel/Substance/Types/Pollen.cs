using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : BaseSubstance
{
    // Example of a Substance
    protected override void Awake()
    {
        substanceType = SubstanceType.Pollen;
    }
}
