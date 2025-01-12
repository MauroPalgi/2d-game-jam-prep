using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private Vector2 direction = Vector2.right;

    void Start() { }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = direction * speed;
    }

    public void SetDirection(Vector2 vector2)
    {
        this.direction = vector2;
        transform.localScale = new Vector3(
            vector2.x * transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Hittable"))
        {
            Destroy(gameObject);
        }
    }
}
