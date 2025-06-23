using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{
    private int id;

    protected virtual void Awake()
    {
        gameObject.layer = 10; // SubstanceInteractable
        RaycastTargetManager.Instance.AddRaycastTarget(this);
    }

    public virtual void OnRaycastEnter(GameObject emitter)
    {

    }
    public virtual void OnRaycastExit(GameObject emitter)
    {

    }

    public int GetId()
    { return id; }

    public void SetId(int id)
    { this.id = id; }
}
