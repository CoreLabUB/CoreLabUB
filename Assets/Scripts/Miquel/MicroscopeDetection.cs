using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeDetection : MonoBehaviour
{
    [SerializeField] private bool activateDrawing;
    [SerializeField] private GameObject pcStartButton;
    void Awake()
    {
    }

    private void Update()
    {
    }

    public void Detect()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(0, 0, 0.18f);
        if (Physics.BoxCast(transform.position - offset, transform.lossyScale, transform.forward, out hit, transform.rotation, 0.1f, LayerMask.GetMask("SubstanceInteractable")))
        {
            if (hit.transform.GetComponent<SubstanceSample>().GetSubstanceType() == SubstanceType.Pollen)
            {
                pcStartButton.SetActive(true);
            }
            else
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        //DrawBoxCast(transform.position + transform.localScale.z * (-transform.forward), transform.position + transform.localScale.z * (-transform.forward) + maxDistance * transform.forward, transform.localScale, transform.rotation );
        if (!activateDrawing) { return; }

        RaycastHit hit;
        Vector3 offset = new Vector3(0, 0, 0.18f);    
        if (Physics.BoxCast(transform.position - offset, transform.lossyScale, transform.forward, out hit, transform.rotation, 0.1f, LayerMask.GetMask("SubstanceInteractable")))
        {
            Gizmos.DrawWireCube(hit.transform.position, transform.lossyScale);
        }
    }

    void DrawBoxCast(Vector3 start, Vector3 end, Vector3 size, Quaternion rotation)
    {
        Gizmos.color = Color.green;

        // Cache the Gizmos matrix.
        Matrix4x4 currentMatrix = Gizmos.matrix;

        // Draw Cubes
        Gizmos.matrix = Matrix4x4.TRS(start, rotation, size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.TRS(end, rotation, size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        // Draw Connecting Lines
        Vector3 x = Vector3.right * size.x * 0.5f;
        Vector3 y = Vector3.up * size.y * 0.5f;
        Vector3 z = Vector3.forward * size.z * 0.5f;
        Gizmos.matrix = Matrix4x4.TRS(start, rotation, Vector3.one);
        Gizmos.DrawRay(Vector3.zero - x - y - z, Vector3.forward * 1);
        Gizmos.DrawRay(Vector3.zero - x + y - z, Vector3.forward * 1);
        Gizmos.DrawRay(Vector3.zero + x - y - z, Vector3.forward * 1);
        Gizmos.DrawRay(Vector3.zero + x + y - z, Vector3.forward * 1);

        // Reset the Gizmos matrix.
        Gizmos.matrix = currentMatrix;
    }
}
