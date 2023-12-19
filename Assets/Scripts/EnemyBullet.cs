using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public int damage = 10;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with the player
        PlayerController player = other.GetComponent<PlayerController>();

        // If the collided object is the player, deal damage to the player
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject); // Destroy the bullet after hitting the player
        }
    }
}
