using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyXRGrabInteractable : MonoBehaviour
{
    private XRGrabInteractable interactable;


    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
