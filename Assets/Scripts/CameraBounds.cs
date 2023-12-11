using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public float padding = 0.5f; // Adjust this value to add padding around the camera bounds

    private Camera mainCamera;

    private void Start()
    {
        FindMainCamera();
    }

    private void LateUpdate()
    {
        if (mainCamera == null)
        {
            FindMainCamera();
            return;
        }

        Vector3 cameraPos = mainCamera.WorldToViewportPoint(transform.position);
        cameraPos.x = Mathf.Clamp01(cameraPos.x);
        cameraPos.y = Mathf.Clamp01(cameraPos.y);

        Vector3 clampedPos = mainCamera.ViewportToWorldPoint(cameraPos);

        // Adjust for padding
        clampedPos.x = Mathf.Clamp(clampedPos.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding);
        clampedPos.y = Mathf.Clamp(clampedPos.y, mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding);

        transform.position = clampedPos;
    }

    private void FindMainCamera()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found. Make sure you have a camera in the scene.");
        }
    }
}
