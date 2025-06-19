using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardReader : MonoBehaviour
{
    // Event that notifies this when palyer is dragging a magnetic card
    public static UnityEvent<bool> toggleCardReaderRaycast = new();

    // Magnetic card being dragged -> true, Magnetic card not being dragged -> false
    private bool raycastsActive = false;

    // 
    private bool doorsOpen = false;

    // Raycast Interactable layermask
    [SerializeField] private LayerMask interactableLayerMask;

    // Perms to open the door
    [SerializeField] CardPerms labPerm;


    // Raycasts variables
    private Vector3 raycastOffsets;
    Ray rayTop;
    Ray rayBot;

    private void Awake()
    {
        toggleCardReaderRaycast.AddListener((bool state) => 
        {
            raycastsActive = state;
        });

        raycastOffsets = new Vector3(0,transform.localScale.y/4, 0);

        rayTop = new Ray(transform.position + raycastOffsets, transform.forward);
        rayBot = new Ray(transform.position - raycastOffsets, transform.forward);
    }

    private void Update()
    {
        if (!raycastsActive) { return; }

        RaycastHit hitTop;
        RaycastHit hitBot;

        // Tow Raycast for better precission
        if (!Physics.Raycast(rayTop, out hitTop, 2.0f, interactableLayerMask))
        { return; }

        if (!Physics.Raycast(rayBot, out hitBot, 2.0f, interactableLayerMask))
        { return; }

        GameObject detectedObject = hitTop.transform.gameObject;

        if (detectedObject.GetComponent<RaycastInteractable>().GetInteractableType() == InteractableType.MagneticCard)
        {
            if (detectedObject.GetComponent<MagneticCard>().CheckPerm(labPerm))
            {
                Debug.Log("CardDetected");
                return;
                // Play Confirmation Sound, Card Reader Panel Emits Green Light
                // Open Door, Wait, Close Door
            }
            else
            {
                // Play Beep Sound, Card Reader Panel Blinks Red Light
            }
        }
    }
}
