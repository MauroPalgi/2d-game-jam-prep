using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private Vector3 originalPosition; // La posición inicial del bloque.
    public float bounceHeight = 0.5f; // Altura del salto.
    public float bounceSpeed = 4f; // Velocidad del salto.
    private bool isBouncing = false;
    private int hits = 0;
    public float bounceForce = 10f;

    void Start()
    {
        // Guarda la posición inicial del bloque.
        originalPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with the block.");

            // Intentar obtener el Rigidbody2D del jugador.
            Rigidbody2D playerRb = collision.GetComponentInParent<Rigidbody2D>();

            if (playerRb != null)
            {
                Debug.Log("Player Rigidbody2D found.");
                // Aplicar fuerza al jugador hacia arriba.
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);

                // Si no está rebotando, iniciar el rebote del bloque.
                if (!isBouncing)
                {
                    hits++;
                    Debug.Log($"Block hit count: {hits}");
                    StartCoroutine(Bounce());
                }
            }
            else
            {
                Debug.LogWarning("Player Rigidbody2D not found!");
            }
        }
    }

    private System.Collections.IEnumerator Bounce()
    {
        isBouncing = true;

        // Subir el bloque.
        while (transform.position.y < originalPosition.y + bounceHeight)
        {
            transform.position += Vector3.up * bounceSpeed * Time.deltaTime;
            yield return null;
        }

        // Bajar el bloque de vuelta a su posición original.
        while (transform.position.y > originalPosition.y)
        {
            transform.position -= Vector3.up * bounceSpeed * Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la posición sea exactamente la original.
        transform.position = originalPosition;
        isBouncing = false;
    }
}
