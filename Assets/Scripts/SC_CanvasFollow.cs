using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanvasFollow : MonoBehaviour
{
    // Asigna la cámara del jugador en el inspector
    public Transform playerCamera;
    // Distancia a la que quieres que aparezca el canvas frente a la cámara
    public float distance = 2.0f;
    // Offset adicional (si lo necesitas para ajustar la posición vertical u horizontal)
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        // Calcula la nueva posición: un punto fijo a 'distance' unidades adelante de la cámara + offset
        transform.position = playerCamera.position + playerCamera.forward * distance + offset;

        // Hace que el canvas mire siempre hacia la cámara
        // Primero, orienta el canvas para que "mire" hacia la cámara
        transform.LookAt(playerCamera);
        // Luego, rota 180 grados para que se vea de frente al jugador
        transform.Rotate(0, 0, 0);
    }
}
