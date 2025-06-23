using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public enum StickState { GetSample, PutSample }
public class Stick : RaycastInteractable
{
    private bool enableDetection = true;
    private bool hasSubstance = false;
    private bool hasHit = false;

    StickState stickState = StickState.GetSample;

    RaycastTarget previousTarget;

    private BaseSubstance substance;

    private Vector3 headPosition  = new Vector3(0,0,0.02f);
    private float rayDistance = 0.05f;

    [SerializeField] protected LayerMask substanceLayer;

    private AudioSource headAudio;

    protected override void Awake()
    {
        base.Awake();
        interactableType = InteractableType.Stick;

        headAudio = transform.GetChild(0).gameObject.GetComponent<AudioSource>();

        headAudio.Play();
        headAudio.Pause();
    }

    public override void Drag()
    {
        if (!enableDetection) { return; }

        Ray ray = new Ray(transform.position + headPosition, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, substanceLayer))
        {
            RaycastTarget targetHit = hit.transform.GetComponent<RaycastTarget>();

            if (previousTarget == null) // Initial Detection
            { previousTarget = targetHit; }

            if (previousTarget.GetId() != targetHit.GetId())
            {
                previousTarget.OnRaycastExit(gameObject);
                previousTarget = targetHit;
            }

            targetHit.OnRaycastEnter(gameObject);

            hasHit = true;
        }
        else
        {
            if (previousTarget == null)
            { return; }

            previousTarget.OnRaycastExit(gameObject);
        }
    }

    public override void Cancel()
    {
        if (!hasHit) { return; }

        previousTarget.OnRaycastExit(gameObject);
        headAudio.Pause();

        hasHit = false;
    }

    public void ChangeHead(Material material)
    {
        transform.GetChild(0).GetComponent<Renderer>().material = material;
    }

    public void SetSubstance(BaseSubstance substanceFound)
    {
        substance = substanceFound;
        stickState = StickState.PutSample;
    }  

    public BaseSubstance GetSubstance()
    { return substance; }
    
    public AudioSource GetHeadAudioSource()
    {
        return headAudio;
    }

    public StickState GetState()
    {
        return stickState;
    }

    public Material GetSubstanceMaterial()
    {
        return substance.GetSubstanceMaterial();
    }

    public void OnDestroy()
    {
        Cancel();
    }
}
