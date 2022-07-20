using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

      
    protected Rigidbody enemyRb;
    protected GameObject player;
    private float xBound = 14;
    private float zBound = 8;
    protected float distance;
 
    protected PlayerController playerController;
    public Transform target;
    public Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
     
        playerController =GameObject.Find("Farmer").GetComponent<PlayerController>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Farmer");
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasFreeze==true)
        {
            enemyRb.velocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;
        }
        else
        {
            Run();// ABSTRACTION
            RestrainEnemy();// ABSTRACTION
        }
    }
    void RestrainEnemy()
    {
        if (transform.position.x < -xBound)
        {
            enemyRb.AddForce(Vector3.right * 5);
        }
        if (transform.position.x > xBound)
        {
            enemyRb.AddForce(Vector3.left * 5);
        }
        if (transform.position.z > zBound)
        {
            enemyRb.AddForce(Vector3.back*5);
        }
        if (transform.position.z < -zBound)
        {
            enemyRb.AddForce(Vector3.forward * 5);
        }
    }
     public virtual  void Run()
    {
         float safeDistance = 5;
         float speed = 0.8f;
        distance = Vector3.Distance(player.transform.position, enemyRb.transform.position);
        if (distance < safeDistance)
        {
            enemyAnim.SetBool("Static_b", false);
            enemyAnim.SetFloat("Speed_f", 0.4f);
            Vector3 lockDirection = (enemyRb.transform.position - player.transform.position).normalized;
      
           // Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lockDirection);
            transform.rotation = rotation;

            enemyRb.AddForce(lockDirection * speed);
           
        }
        else
        {
            enemyAnim.SetFloat("Speed_f", 0.1f);
        }
    }
    
}
