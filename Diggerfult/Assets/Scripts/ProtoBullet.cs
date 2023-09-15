using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // TODO : Shoot의 대안 찾기
        transform.position = startPoint;
        Vector2 direction = (endPoint - startPoint).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == StaticLayerMask.ExitField)
        {
            ObjectPool.inst.ReleaseObject<ProtoBullet>(this.gameObject);
        }
        else if (collision.gameObject.layer == StaticLayerMask.BlockField)
        {
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
        else if (collision.gameObject.layer == StaticLayerMask.ActionBlock)
        {
            // TODO : Action
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
        else if (collision.gameObject.layer == StaticLayerMask.ObstacleBlock)
        {
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
    }
}
