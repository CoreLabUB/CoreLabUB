using Unity.VisualScripting;
using UnityEngine;


public enum SubstanceType { NULL, Pollen, Wine, Grass }
public class BaseSubstance : RaycastTarget
{
    [SerializeField] protected SubstanceType substanceType;

    protected Material substanceMaterial;

    protected override void Awake()
    {
        base.Awake();

        switch (substanceType)
        {
            
            case SubstanceType.Pollen:
            {
                transform.AddComponent<Pollen>().ApplyMaterial(substanceType);

                break;
            }
            case SubstanceType.Wine:
            {
                transform.AddComponent<Wine>().ApplyMaterial(substanceType);

                break;
            }
            case SubstanceType.Grass:
            {
                transform.AddComponent<Grass>().ApplyMaterial(substanceType);
                
                break;
            }
            case SubstanceType.NULL:
                break;
        }
        Destroy(this);
    }

    public SubstanceType GetSubstanceType()
    {
        return substanceType;
    }

    protected void ApplyMaterial(SubstanceType substanceType)
    {
        substanceMaterial = (Material) Resources.Load("Materials/Substances/" + substanceType.ToString());
        GetComponent<Renderer>().material = substanceMaterial;
    }

    public Material GetSubstanceMaterial()
    {
        return substanceMaterial;
    }

    public override void OnRaycastEnter(GameObject emitter)
    {
        emitter.GetComponent<Stick>().ChangeHead(substanceMaterial);
        emitter.GetComponent<Stick>().SetSubstance(this);

        VRRaycastInteraction.right_resetHandAnimation.Invoke();
        VRRaycastInteraction.left_resetHandAnimation.Invoke();

        Destroy(gameObject);
    }
}
