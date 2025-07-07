using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : BaseSubstance
{
    // Example of a Substance
    protected override void Awake()
    {
        substanceType = SubstanceType.Grass;
    }
}
