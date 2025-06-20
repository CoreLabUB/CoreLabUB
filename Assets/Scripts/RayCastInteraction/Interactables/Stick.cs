using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stick : RaycastInteractable
{
    private bool enableDetection = true;

    private BaseSubstance substance;

    private Vector3 headPosition  = new Vector3(0,0,0.02f);
    private float rayDistance = 0.05f;

    [SerializeField] protected LayerMask substanceLayer;
    [SerializeField] protected LayerMask substanceObjectLayer;

    GameObject detectedSubstance;

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

        SwippingHeadAudio(ray);

        Debug.DrawRay(transform.position + headPosition, transform.forward, Color.blue);

        if (!Physics.Raycast(ray, out hit, rayDistance, substanceLayer))
        { return; }

        // Found Substance
        substance = hit.transform.GetComponent<BaseSubstance>();

        AudioManager.Instance.PlaySoundAt("StickSuccess", hit.transform.position);
        headAudio.Stop();

        ChangeHead();

        Destroy(hit.transform.gameObject);

        enableDetection = false;
    }

    public override void Cancel()
    {
        headAudio.Pause();
    }

    private void ChangeHead()
    {
        transform.GetChild(0).GetComponent<Renderer>().material = substance.GetSubstanceMaterial();
    }

    private void SwippingHeadAudio(Ray ray)
    {
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, rayDistance, substanceObjectLayer))
        {
            headAudio.Pause();
        }
        else
        {
            headAudio.UnPause();
        }
    }
}
