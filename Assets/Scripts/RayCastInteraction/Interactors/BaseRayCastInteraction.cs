using UnityEngine;

/* ---------------------------------
    MADE BY MIQUEL FORCADA MERCADE
    https://www.linkedin.com/in/miquel-forcada-mercade/
*/


public class BaseRaycastInteraction : MonoBehaviour
{
    protected XRIDefaultInputActions playerInputs;

    protected Camera playerCamera;

    private void Awake()
    {
        playerInputs = new XRIDefaultInputActions();
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
