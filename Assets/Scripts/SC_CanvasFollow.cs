using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanvasFollow : MonoBehaviour
{
    // Asigna la c�mara del jugador en el inspector
    public Transform playerCamera;
    // Distancia a la que quieres que aparezca el canvas frente a la c�mara
    public float distance = 2.0f;
    // Offset adicional (si lo necesitas para ajustar la posici�n vertical u horizontal)
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        // Calcula la nueva posici�n: un punto fijo a 'distance' unidades adelante de la c�mara + offset
        transform.position = playerCamera.position + playerCamera.forward * distance + offset;

        // Hace que el canvas mire siempre hacia la c�mara
        // Primero, orienta el canvas para que "mire" hacia la c�mara
        transform.LookAt(playerCamera);
        // Luego, rota 180 grados para que se vea de frente al jugador
        transform.Rotate(0, 0, 0);
    }
}
