using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerScript : MonoBehaviour
{
    private Rigidbody player;

    private AudioSource audioSource;
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip death;

    public SceneManagerScript sceneManagerScript;

    GameObject InvincibleIndic;
    public GameObject InvinciblePUPmini;
    Vector3 invincibleIndicator = new Vector3(7, 5.75f, 0);

    public float VerticalVelocity;
    public float gravity = 50.0f;
    public float jumpForce = 5.0f;
    public float playerSpeed = 10;

    public int timer = 0;
    public bool invincibleMode = false;

    public float PlayerPosition { get; private set; }
    public bool grounded = true;
    private Vector3 oldPosition = new Vector3(0, 0, 0);

    public bool dead = false;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
        PlayerJump();
        IsPowerUp();
        //AreYouDead();
    }

    private void PlayerMove()
    {
        PlayerPosition = Input.GetAxis("Horizontal");
        Debug.Log(PlayerPosition);

        Vector3 newPosition = transform.position;

        if (Input.GetButton("Fire1"))
        {
            playerSpeed = 15;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.mass = 2;
        }
        else
        {
            playerSpeed = 10;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.mass = 1;
        }

        newPosition.x += PlayerPosition * Time.deltaTime * playerSpeed;

        newPosition.x = Mathf.Clamp(newPosition.x, -30, 30);
        transform.position = newPosition;


    }

    void IsPowerUp()
    {
        if (invincibleMode == true)
        {
            timer++;
            if (timer >= 1000)
            {
                invincibleMode = false;
                timer = 0;
                Destroy(InvincibleIndic);
            }
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            grounded = true;
        }

        if (other.gameObject.tag == "Invincible")
        {
            invincibleMode = true;
            Destroy(other.gameObject);
            InvincibleIndic = Instantiate(InvinciblePUPmini, invincibleIndicator, Quaternion.identity);
            audioSource.PlayOneShot(collect);
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (invincibleMode == true)
            {
                Destroy(other.gameObject);
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.mass = 50;
            }
            else
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.mass = 1;
            }
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            grounded = false;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded == true)
            {
                player.velocity = (jumpForce * Vector3.up);
                audioSource.PlayOneShot(jump);
            }
        }
    }

    //private void AreYouDead()
    //{
    //    if (transform.position.y < -4)
    //    {
    //        //sceneManagerScript.YouDied();
    //        dead = true;
    //    }
    //}
}
