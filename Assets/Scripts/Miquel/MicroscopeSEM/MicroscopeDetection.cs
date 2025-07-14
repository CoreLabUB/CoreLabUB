using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeDetection : MonoBehaviour
{
    [SerializeField] private bool activateDrawing;
    [SerializeField] private GameObject startScanButton;

    private SubstanceType sampleHit;

    // Detects the interior of the SEM
    public void Detect()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(0, 0, 0.18f);

        // If there is an object with the SubstanceInteractable layer it continues

        if (Physics.BoxCast(transform.position - offset, transform.lossyScale, transform.forward, out hit, transform.rotation, 0.1f, LayerMask.GetMask("SubstanceInteractable")))
        {
            // Maybe add a Switch for every SubstanceType and other behaviors

            if (hit.transform.GetComponent<SubstanceSample>().GetSubstanceType() == SubstanceType.Pollen)
            {
                startScanButton.SetActive(true);

                sampleHit = hit.transform.gameObject.GetComponent<SubstanceSample>().GetSubstanceType();

                startScanButton.transform.parent.GetComponent<MinigameSEM>().SetSubstanceType(sampleHit);
                startScanButton.transform.parent.GetComponent<MinigameSEM>().SetSampleMicroscope(hit.transform.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!activateDrawing) { return; }

        RaycastHit hit;
        Vector3 offset = new Vector3(0, 0, 0.18f);    
        if (Physics.BoxCast(transform.position - offset, transform.lossyScale, transform.forward, out hit, transform.rotation, 0.1f, LayerMask.GetMask("SubstanceInteractable")))
        {
            Gizmos.DrawWireCube(hit.transform.position, transform.lossyScale);
        }
    }
}
