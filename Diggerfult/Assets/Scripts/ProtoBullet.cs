using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBullet : MonoBehaviour
{
    // TODO : BulletInfo를 고안해야 함
    private float bulletSpeed = 50.0f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 startPoint, Vector2 endPoint)
    {
        transform.position = startPoint;
        Vector2 direction = (endPoint - startPoint).normalized;
        StartCoroutine(Shooting(direction));
    }

    IEnumerator Shooting(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        if (collision.gameObject.tag == "BlockField")
        {
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
    }
}
