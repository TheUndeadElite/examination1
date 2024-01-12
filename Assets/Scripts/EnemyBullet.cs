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
        PlayerController player = other.GetComponent<PlayerController>();
        PlayerBlocking playerBlocking = other.GetComponentInChildren<PlayerBlocking>();

        // If the collided object is the player and the shield is not active, deal damage to the player
        if (player != null && playerBlocking != null)
        {
            if(!playerBlocking.IsShieldActive())
            {
                player.TakeDamage(damage);
                Destroy(gameObject); // Destroy the bullet after hitting the player
            }
           
        }
    }
}
