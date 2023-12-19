using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData CurrentPlayerData = null;
    public TextMeshProUGUI HPText = null;
    public TextMeshProUGUI PointText = null;

    [SerializeField]
    float moveSpeed = 6;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;

    [SerializeField]
    float shakeAmount = 0.1f;

    [SerializeField]
    float shakeDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (CurrentPlayerData != null)
        {
            CurrentPlayerData.HP = 100;
            HPText.text = CurrentPlayerData.HP + " HP";
            CurrentPlayerData.Points = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        moveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        HPText.text = CurrentPlayerData.HP + " HP";
        PointText.text = CurrentPlayerData.Points.ToString() + " Points";

        


    }

    private void FixedUpdate()
    {
        // Move the player
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.deltaTime;
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        pos += move;

        transform.position = pos;

        // Handle shooting logic here if needed
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        if (CurrentPlayerData != null)
        {
            // Shake the player when taking damage
            StartCoroutine(Shake());

            CurrentPlayerData.HP -= damage;

            // Ensure HP doesn't go below 0
            CurrentPlayerData.HP = Mathf.Max(0, CurrentPlayerData.HP);

            // Update HP text
            HPText.text = CurrentPlayerData.HP + " HP";

            Debug.Log("Player took damage! Current HP: " + CurrentPlayerData.HP);

            // Add any additional logic for when the player's HP changes
        }
    }

    IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = transform.position.x + Random.Range(-shakeAmount, shakeAmount);
            float y = transform.position.y + Random.Range(-shakeAmount, shakeAmount);

            transform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition; // Reset to the original position
    }

    // Function to handle collisions with enemies
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with an enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Deal damage to the player
            TakeDamage(5);

            // Destroy the enemy
            Destroy(enemy.gameObject);
        }
    }
}
