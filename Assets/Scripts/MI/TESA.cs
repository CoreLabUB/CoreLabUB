using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESA : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Transform of the player to follow.")]
    public Transform player;

    [Header("Movement Settings")]
    [Tooltip("Movement speed in units per second.")]
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        // Ensure BoxCollider exists
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Calculate target position: match player's X and Z, keep own Y (-4)
        Vector3 currentPosition = rb.position;
        Vector3 targetPosition = new Vector3(player.position.x, currentPosition.y, player.position.z);

        // Move towards the target using Rigidbody for physics consistency
        Vector3 direction = (targetPosition - currentPosition).normalized;
        Vector3 newPosition = currentPosition + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
