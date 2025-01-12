using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int damageToPlayer = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthScript playerHealth = collision.gameObject.GetComponent<HealthScript>();
            Debug.Log("Player Current help" + playerHealth.GetCurrentHealth());
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
        }
    }
}
