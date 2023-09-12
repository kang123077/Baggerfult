using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoBullet : MonoBehaviour
{
    // TODO : BulletInfo를 고안해야 함
    public LayerMask exitMask;
    public LayerMask fieldMask;
    public LayerMask actionMask;
    public LayerMask obstacleMask;
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
        Debug.Log("OnCollisionEnter2D");
        if ((1 << collision.gameObject.layer & exitMask) != 0)
        {
            ObjectPool.inst.ReleaseObject<ProtoBullet>(this.gameObject);
        }
        else if ((1 << collision.gameObject.layer & fieldMask) != 0)
        {
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
        else if ((1 << collision.gameObject.layer & actionMask) != 0)
        {
            // TODO : Action
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
        else if ((1 << collision.gameObject.layer & obstacleMask) != 0)
        {
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;
        }
    }
}
