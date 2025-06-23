using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SubstanceType { NULL, Pollen }
public class BaseSubstance : RaycastTarget
{
    protected SubstanceType substanceType;

    [SerializeField] private Material substanceMaterial;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<Renderer>().material = substanceMaterial;
    }

    public SubstanceType GetSubstanceType()
    {
        return substanceType;
    }

    public Material GetSubstanceMaterial()
    {
        return substanceMaterial;
    }

    public override void OnRaycastEnter(GameObject emitter)
    {
        emitter.GetComponent<Stick>().ChangeHead(substanceMaterial);
        emitter.GetComponent<Stick>().SetSubstance(this);
        Destroy(gameObject);
    }
}
