using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy //INHERITANCE
{
    public override void Run() // POLYMORPHISM
    {
        float safeDistance = 4;
        float speed = 0.7f;
        distance = Vector3.Distance(player.transform.position, enemyRb.transform.position);
        if (distance < safeDistance)
        {
            enemyAnim.SetBool("Static_b", false);
            enemyAnim.SetFloat("Speed_f", 0.4f);
            Vector3 lockDirection = (enemyRb.transform.position - player.transform.position).normalized;

            enemyRb.AddForce(lockDirection * speed);

            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lockDirection);
            transform.rotation = rotation;
        }
        else
        {
            enemyAnim.SetFloat("Speed_f", 0.1f);
        }
    }

}
