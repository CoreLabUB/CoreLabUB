using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SubstanceType { NULL, Pollen }
public class BaseSubstance : MonoBehaviour
{
    protected SubstanceType substanceType;

    [SerializeField] private Material substanceMaterial;

    void Awake()
    {
        gameObject.layer = 10; // SubstanceInteractable
    }

    public SubstanceType GetSubstanceType()
    {
        return substanceType;
    }

    public Material GetSubstanceMaterial()
    {
        return substanceMaterial;
    }
}
