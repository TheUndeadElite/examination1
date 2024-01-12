using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject OffScreenChecker = null;

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float shakeAmount = 0.1f;

    [SerializeField]
    private float shakeDuration = 0.2f;

    [SerializeField]
    private int pointsOnDeath = 0;

    // Reference to the current player's data
    public PlayerData CurrentPlayerData = null;

    void Update()
    {
        if (OffScreenChecker != null && OffScreenChecker.transform.position.x > transform.position.x)
        {
            // The enemy is past the designated edge, deal damage to the player and destroy the enemy
            DealDamage();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentHealth = maxHealth;

        if(OffScreenChecker == null)
        {
            OffScreenChecker = GameObject.Find("OffScreenChecker");

            if (OffScreenChecker == null)
            {
                Debug.LogError("OffScreenChecker not found. Please assign it in the inspector or ensure it exists in the scene.");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Shake the enemy when taking damage
            StartCoroutine(Shake());
        }
    }

    void Die()
    {
        CurrentPlayerData.AddPoints(pointsOnDeath);

        // Add any death behavior here, such as playing an animation, spawning particles, etc.
        Destroy(gameObject);
    }

    IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-shakeAmount, shakeAmount);
            float y = originalPosition.y + Random.Range(-shakeAmount, shakeAmount);

            transform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition; // Reset to the original position
    }

    // Function to deal damage to the player
    public void DealDamage()
    {
        if (CurrentPlayerData != null)
        {
            CurrentPlayerData.HP -= 10; // Adjust the damage amount as needed
            Debug.Log("Player took damage! Current health: " + CurrentPlayerData.HP );
        }
    }

    // Handle collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected"); // Add this line for debugging

        PlayerController playerComp = collision.gameObject.GetComponent<PlayerController>();
        if (playerComp != null)
        {
            DealDamage();
        }
    }
}
