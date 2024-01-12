using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBlocking : MonoBehaviour
{
    public SpriteRenderer blockingImage;
    public TextMeshProUGUI cooldownText;
    public float blockDuration = 2f;
    public float cooldownDuration = 5f;

    private bool isBlocking = false;

    private void Start()
    {
        // Ensure the blocking image is initially disabled
        blockingImage.enabled = false;

        // Start with the shield ready
        UpdateCooldownText(0);
    }

    private void Update()
    {
        // Check for player input to trigger blocking
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !isBlocking || Input.GetKey(KeyCode.X))
        {
            StartBlocking();
        }
    }

    private void StartBlocking()
    {
        // Display "Shield Ready" at the beginning
        UpdateCooldownText(0);

        // Start the blocking coroutine
        StartCoroutine(BlockCoroutine());
    }

    private IEnumerator BlockCoroutine()
    {
        // Enable the blocking image
        blockingImage.enabled = true;
        isBlocking = true;

        // Wait for the block duration
        yield return new WaitForSeconds(blockDuration);

        // Disable the blocking image after the block duration
        blockingImage.enabled = false;

        isBlocking = false;
        // Start the cooldown coroutine
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        isBlocking = false;
        float cooldownTimer = cooldownDuration;

        while (cooldownTimer > 0)
        {
            // Update the cooldown text
            UpdateCooldownText(cooldownTimer);

            // Decrease the cooldown timer
            cooldownTimer -= Time.deltaTime;

            yield return null;
        }

        // Ensure the cooldown text shows "Shield Ready" when the cooldown is complete
        UpdateCooldownText(0);

        // Reset the blocking state

    }

    private void UpdateCooldownText(float timeRemaining)
    {
        // Update the UI text with the remaining cooldown time or "Shield Ready"
        cooldownText.text = timeRemaining > 0 ? $"Cooldown: {timeRemaining:F1}s" : "Shield Ready";
    }

    // Method to check if the shield is currently active
    public bool IsShieldActive()
    {
        return isBlocking;
    }
}
