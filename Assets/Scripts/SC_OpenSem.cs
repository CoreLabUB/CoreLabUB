using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    public float rotationSpeed = 2f; // Velocidad de apertura
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        // Guardamos la rotaci�n inicial como "cerrada"
        closedRotation = transform.localRotation;
        openRotation = Quaternion.Euler(0, 180, 0); // Rotaci�n abierta
    }


    public void ToggleDoor()
    {
        StopAllCoroutines(); // Detiene cualquier animaci�n en curso
        StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
        isOpen = !isOpen;
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.localRotation = targetRotation; // Asegurarse de que llegue exactamente
    }

    public bool GetSemIsOpen()
    {
        return isOpen;
    }
}
