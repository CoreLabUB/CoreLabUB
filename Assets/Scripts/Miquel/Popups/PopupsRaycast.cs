using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsRaycast : MonoBehaviour
{
    [SerializeField] protected int popupId;

    protected Ray ray;
    [SerializeField] protected float rayDistance;

    [SerializeField] protected LayerMask interactableLayerMask;

    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
    }

    private void OnDrawGizmos()
    {
        ray = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, transform.forward * rayDistance);
    }
}
