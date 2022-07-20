using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{ 
     float xBound = 14;
     float zBound = 8;

    private GameManager addTime;
  
    public bool hasFreeze = false;
    public TextMeshProUGUI addSpeedText;
    private int speedTime;
    public TextMeshProUGUI addFreezeText;
    private int freezeTime;

    private AudioSource playerAudio;
    public ParticleSystem freezeParticle;
    public ParticleSystem speedupParticle;
    public AudioClip pickupPowerupSound;
    public AudioClip pickupChickenSound;
    private Animator playerAnim;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 6f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        addTime = GameObject.Find("GameManager").GetComponent<GameManager>();

        controller = gameObject.AddComponent<CharacterController>();
        controller.center = new Vector3(0, 1.42f, 0);
        controller.height = 2.75f;
    }

    // Update is called once per frame
    void Update()
    {
       MovePlayer();// ABSTRACTION
        RestrainPlayer();// ABSTRACTION
        SetAnimation();// ABSTRACTION
    }

    void SetAnimation()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerAnim.SetBool("Static_b", false);
            playerAnim.SetFloat("Speed_f", 0.4f);
        }
        else
        {
            playerAnim.SetFloat("Speed_f", 0.1f);
        }
    }
  
    void MovePlayer()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    void RestrainPlayer()
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(pickupChickenSound, 1.0f);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Addtime"))
        {
            playerAudio.PlayOneShot(pickupPowerupSound, 2.0f);
            Destroy(other.gameObject);
            addTime.time += 5;
        }
        if (other.gameObject.CompareTag("AddSpeed"))
        {
            speedupParticle.Play();
            playerAudio.PlayOneShot(pickupPowerupSound, 2.0f);
            Destroy(other.gameObject);
            StartCoroutine(AddSpeedCountdownRoutine());
        }
        if (other.gameObject.CompareTag("Freeze"))
        {
            freezeParticle.Play();
            playerAudio.PlayOneShot(pickupPowerupSound, 2.0f);
            Destroy(other.gameObject);
           
            hasFreeze = true;
            StartCoroutine(FreezeCountdownRoutine());
        }
    }
    IEnumerator AddSpeedCountdownRoutine()
    {
        speedTime += 5;
        while (speedTime>=0)
        {
            playerAnim.SetFloat("Speed_f", 0.8f);
            addSpeedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            addSpeedText.text = "Speed up: " + speedTime;
            speedTime--;
            playerSpeed = 12;

        }
        playerAnim.SetFloat("Speed_f", 0.4f);
        playerSpeed =6;
        speedupParticle.Stop();
        addSpeedText.gameObject.SetActive(false);
    }
    IEnumerator FreezeCountdownRoutine()
    {
        freezeTime += 5;
        while (freezeTime >= 0)
        {
            addFreezeText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            addFreezeText.text = "Freeze effect: : " + freezeTime;
            freezeTime--;
        }
        freezeParticle.Stop();
        addFreezeText.gameObject.SetActive(false);
        hasFreeze = false;
    }

}

