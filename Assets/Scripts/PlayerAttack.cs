using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject proyectilePrefab;

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private float attackDelay = 0.25f;

    [SerializeField]
    private int damageToEnemy = 20;

    private float lastAttack;

    void Start() { }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > lastAttack + attackDelay)
        {
            Attack();
            lastAttack = Time.time;
        }
    }

    private void Attack()
    {
        Vector3 direction = GetPlayerDirection();
        GameObject bulletIntance = Instantiate(
            proyectilePrefab,
            firePoint.transform.position + (direction * 0.1f),
            Quaternion.identity
        );
        bulletIntance.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        bulletIntance.GetComponent<BulletScript>().SetDirection(direction);
    }

    private Vector2 GetPlayerDirection()
    {
        // (0.00873, 0.99992, -0.00873, 0.00038) izquierda
        // (0.00000, -0.00046, 0.00000, 1.00000)
        Vector2 direction = Vector2.zero;
        if (transform.rotation.z < 0)
        {
            direction = Vector2.left;
        }
        if (transform.rotation.y < 0)
        {
            direction = Vector2.right;
        }

        return direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hittable"))
        {
            HealthScript enemyHealth = collision.gameObject.GetComponent<HealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageToEnemy);
            }
        }
    }



}
