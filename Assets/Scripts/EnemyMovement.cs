using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = -3.0f;
    [SerializeField] float XValueToReach = 7.0f;
    [SerializeField] float minY = -2.0f;
    [SerializeField] float maxY = 2.0f;
    [SerializeField] float verticalMoveDuration = 2.0f;

    private float timer = 0.5f;
    private bool movingUp = false;

    private void Update()
    {
        var enemyPos = transform.position;

        if (enemyPos.x > XValueToReach)
        {
            // Move horizontally
            enemyPos.x += MoveSpeed * Time.deltaTime;
        }
        else
        {
            if(enemyPos.y > maxY)
            {
                movingUp = false;
            }
            else if(enemyPos.y < minY)
            {
                movingUp = true;
            }

            float direction = 1.0f;
            if(movingUp)
            {
                direction = 1.0f;
            }
            else
            { 
                direction = -1.0f;
            }

            enemyPos.y += -MoveSpeed * Time.deltaTime* direction;
    
        }

        transform.position = enemyPos;
    }
}
