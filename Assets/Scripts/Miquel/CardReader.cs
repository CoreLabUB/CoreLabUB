using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardReader : MonoBehaviour
{
    // Event that notifies this when palyer is dragging a magnetic card or doors are open
    public static UnityEvent<bool> toggleCardReaderRaycast = new();
    public static UnityEvent<CardPerms, bool> toggleSingleCardReaderRaycast = new();

    // Magnetic card being dragged -> true, Magnetic card not being dragged -> false
    private bool raycastsActive = false;

    // Raycast Interactable layermask
    [SerializeField] private LayerMask interactableLayerMask;

    // Perms to open the door
    [SerializeField] private CardPerms labPerm;

    // Door
    [SerializeField] DoorLab door;

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

        toggleSingleCardReaderRaycast.AddListener((CardPerms cardPerms, bool state) =>
        {
            if (cardPerms != labPerm || !raycastsActive)
            { return; }

            raycastsActive = state;
        });

        raycastOffsets = new Vector3(0,transform.localScale.y/4, 0);

        rayTop = new Ray(transform.position + raycastOffsets, transform.forward);
        rayBot = new Ray(transform.position - raycastOffsets, transform.forward);

        door.SetPerm(labPerm);
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

                AudioManager.Instance.PlaySoundAt("CardReaderConfirmation", transform.position);
                door.OpenDoor();

                raycastsActive = false;
                return;
                // Play Confirmation Sound, Card Reader Panel Emits Green Light
                // Open Door, Wait, Close Door
            }
            else
            {
                AudioManager.Instance.PlaySoundAt("CardReaderError", transform.position);
                // Play Beep Sound, Card Reader Panel Blinks Red Light
            }
        }
    }
}
