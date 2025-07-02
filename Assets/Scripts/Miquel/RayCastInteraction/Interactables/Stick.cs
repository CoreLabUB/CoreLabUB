using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


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
    private float rayDistance = 0.08f;

    [SerializeField] protected LayerMask substanceLayer;

    private AudioSource headAudio;

    protected override void Awake()
    {
        base.Awake();
        interactableType = InteractableType.Stick;

        headAudio = transform.GetChild(0).gameObject.GetComponent<AudioSource>();

        headAudio.Play();
        headAudio.Pause();

        GetComponent<XRGrabInteractable>().hoverEntered.AddListener(_ =>
        {
            HoverEnter(_.interactorObject.transform.gameObject);
        });

        GetComponent<XRGrabInteractable>().hoverExited.AddListener(_ =>
        {
            HoverExit(_.interactorObject.transform.gameObject);
        });

        GetComponent<XRGrabInteractable>().selectEntered.AddListener(_ => 
        {
            StopAllCoroutines();

            SelectEnter(_.interactorObject.transform.gameObject);

            StartCoroutine(Grab());
        });

        GetComponent<XRGrabInteractable>().selectExited.AddListener(_ =>
        {
            StopAllCoroutines();

            SelectExit(_.interactorObject.transform.gameObject);
        });
    }

    public override void SelectEnter(GameObject hand)
    {
        base.SelectEnter(hand);
    }

    public override IEnumerator Grab()
    {
        if (!enableDetection) { yield return null; }

        while(isDragging)
        {
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
                if (previousTarget != null)
                {
                    previousTarget.OnRaycastExit(gameObject);
                }
            }

            yield return null;
        }

        yield return null;
    }

    public override void SelectExit(GameObject hand)
    {
        base.SelectExit(hand);

        if (!hasHit) { return; }

        previousTarget.OnRaycastExit(gameObject);
        headAudio.Pause();

        hasHit = false;
    }

    public void ChangeHead(Material material)
    {
        List<Material> newMaterial = new List<Material>();
        newMaterial.Add(transform.GetComponent<Renderer>().materials[0]);
        newMaterial.Add(material);

        transform.GetComponent<Renderer>().SetSharedMaterials(newMaterial);
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

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position + headPosition, transform.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray);
    }
    public void OnDestroy()
    {
        if (!hasHit) { return; }

        previousTarget.OnRaycastExit(gameObject);
        headAudio.Pause();

        hasHit = false;
    }
}
