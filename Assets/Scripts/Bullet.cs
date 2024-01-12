using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public int damage = 10;

    private Rigidbody2D rb;
    public PlayerData CurrentPlayerData = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        CheckBulletBounds();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            CurrentPlayerData.AddPoints(10);
            Destroy(gameObject); // Destroy the bullet after hitting an enemy
        }
    }

    void CheckBulletBounds()
    {
        // Check if the bullet's X coordinate is greater than 9
        if (transform.position.x > 9)
        {
            Destroy(gameObject);
        }
    }
}
