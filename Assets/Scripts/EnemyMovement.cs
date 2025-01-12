using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float jumpCooldownMin = 1f; // Tiempo mínimo entre saltos

    [SerializeField]
    private float jumpCooldownMax = 3f; // Tiempo máximo entre saltos

    private bool lastXPlayerPosition;

    private float nextJumpTime = 0f; // Control de tiempo para el siguiente salto

    void Update()
    {
        if (player)
        {
            Vector3 direction = player.transform.position - transform.position;

            // Girar al enemigo hacia el jugador
            if (lastXPlayerPosition != direction.x < 0)
            {
                lastXPlayerPosition = direction.x < 0;
                float newDirection = lastXPlayerPosition ? -180 : 180;
                transform.Rotate(0, newDirection, 0);
            }

            // Saltar hacia el jugador
            if (Time.time >= nextJumpTime)
            {
                HandleJump(direction);
                nextJumpTime = Time.time + Random.Range(jumpCooldownMin, jumpCooldownMax);
            }
        }
    }

    private void HandleJump(Vector3 directionToPlayer)
    {
        // Añadir un componente aleatorio a la dirección del salto
        Vector2 randomOffset = new Vector2(Random.Range(-0.5f, 0.5f), 0f);
        Vector2 jumpDirection = new Vector2(directionToPlayer.x, 1f).normalized + randomOffset;

        // Aplicar fuerza de salto
        rigidbody.velocity = jumpDirection * jumpForce;
    }
}
