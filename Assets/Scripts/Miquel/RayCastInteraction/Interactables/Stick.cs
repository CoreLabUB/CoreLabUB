using System.Collections;
using UnityEngine;


public enum StickState { GetSample, PutSample }
public class Stick : RaycastInteractable
{
    private bool enableDetection = true;
    private bool hasHit = false;

    private StickState stickState = StickState.GetSample;

    private RaycastTarget previousTarget;

    private BaseSubstance substance;

    private Vector3 headPosition  = new Vector3(0,0,0.02f);
    private float rayDistance = 0.08f;

    [SerializeField] protected LayerMask substanceLayer;

    private AudioSource headAudio;

    protected override void Awake()
    {
        base.Awake();
        interactableType = InteractableType.Stick;

        headAudio = transform.GetChild(1).gameObject.GetComponent<AudioSource>();

        headAudio.Play();
        headAudio.Pause();
    }

    public override void SelectEnter(GameObject hand)
    {
        base.SelectEnter(hand);
    }

    public override IEnumerator Grab()
    {
        // Performance Optimization
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

                // Check if the targetHit is different from previousTarget to call previousTarget's OnRaycastExit
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

        // Performance Optimization
        if (!hasHit) { return; }

        previousTarget.OnRaycastExit(gameObject);
        headAudio.Pause();

        hasHit = false;
    }

    public void ChangeHead(Material material)
    {
        transform.GetChild(0).GetComponent<Renderer>().material = material;
    }

    // Sets substance for future operations
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
        Gizmos.color = Color.blue;
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
