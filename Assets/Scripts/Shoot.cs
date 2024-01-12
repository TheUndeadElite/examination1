using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class Shoot : MonoBehaviour
{
    public Transform ShootingPoint;
    public GameObject BulletPrefab;

    // Update is called once per frame
    void Update()
        
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Input.GetKey(KeyCode.Z))
        {
            Destroy(Instantiate(BulletPrefab, ShootingPoint.position, transform.rotation), 3f);
        }
    }
}
