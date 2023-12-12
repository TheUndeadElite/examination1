using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float shakeAmount = 0.1f;

    [SerializeField]
    private float shakeDuration = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
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
}
