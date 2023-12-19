using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f; // Time between shots
    private float shootTimer = 0f;

    void Update()
    {
        // Update the shoot timer
        shootTimer += Time.deltaTime;

        // Check if it's time to shoot
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f; // Reset the timer
        }
    }

    void Shoot()
    {
        // Instantiate a bullet at the firePoint position and with a rotation to move from right to left
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 180f));

        // Adjust the bullet speed or other properties if needed
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        if (bulletScript != null)
        {
            // You can adjust bullet properties here, for example:
            // bulletScript.speed = 5f;
        }
    }
}
