using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy // INHERITANCE
{
    private bool isGrounded;
    private float jumpForce=3;


    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void FixedUpdate()
    {
        if (playerController.hasFreeze == false)
        {
            Jump();// ABSTRACTION
        }  
    }

     void Jump()
    {
        distance = Vector3.Distance(player.transform.position, enemyRb.transform.position);
        if (distance < 4 && isGrounded)
        {
            enemyRb.AddForce(Vector3.up * jumpForce,ForceMode.VelocityChange);
            isGrounded = false;

            Vector3 lockDirection = (enemyRb.transform.position - player.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(lockDirection);
            transform.rotation = rotation;

        }

        if (transform.position.y > 3)
        {
            enemyRb.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
    }
    


}
