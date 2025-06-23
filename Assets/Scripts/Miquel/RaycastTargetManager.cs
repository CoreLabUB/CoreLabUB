using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTargetManager : MonoBehaviour
{
    private static RaycastTargetManager instance;
    public static RaycastTargetManager Instance { get { if (instance == null) { instance = new RaycastTargetManager(); } return instance; } }

    private Dictionary<int, RaycastTarget> activeRaycastTargets = new();

    private int currentId = 0;


    public void AddRaycastTarget(RaycastTarget raycasTarget)
    {
        raycasTarget.SetId(currentId);

        activeRaycastTargets.Add(currentId, raycasTarget);

        currentId++;
    }

    public void RemoveRaycasTarget(int id)
    { activeRaycastTargets.Remove(id); }

    public RaycastTarget GetRaycastTargetById(int id) 
    {  return activeRaycastTargets[id]; }
}
