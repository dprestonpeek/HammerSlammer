using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HammerScript : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip smash;
    public AudioClip collect;

    public GameObject SlamZone;

    GameObject SuperHammerIndic;
    public GameObject SuperHammerPUPmini;
    Vector3 superHammerIndicator = new Vector3(7, 5.75f, 0);

    enum Direction { LEFT, RIGHT };
    Direction Dir;
    enum Swing { UP, DOWN, NEUTRAL };
    Swing swing;

    Vector3 parentOldPosition = new Vector3(0, 0, 0);
    Vector3 newPosition;
    Quaternion newRotation;

    public float timer = 0;
    public bool superHammerMode = false;
    public bool invincibleMode = false;


    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHammer();
        SuperHammer();
    }

    void MoveHammer()
    {
        newRotation = transform.localRotation;
        Vector3 parentNewPosition = transform.position;
        newPosition = transform.localPosition;


        if (parentNewPosition.x > parentOldPosition.x + .001)
        {
            Dir = Direction.RIGHT;
            newPosition.x = 0.8f;
            if (swing == Swing.UP)
                newRotation.z = 0;
        }
        if (parentNewPosition.x + .001 < parentOldPosition.x)
        {
            Dir = Direction.LEFT;
            newPosition.x = -0.8f;
            if (swing == Swing.UP)
                newRotation.z = 0;
        }

        transform.localRotation = newRotation;
        transform.localPosition = newPosition;
        parentOldPosition = parentNewPosition;

        if (Input.GetButtonDown("Fire1"))
            audioSource.PlayOneShot(smash);

        if (Input.GetButton("Fire1"))
            swing = Swing.DOWN;
        else
            swing = Swing.UP;
        SlamHammer();
    }

    void SlamHammer()
    {
        newRotation = transform.localRotation;

        if (swing == Swing.DOWN)
        {
            if (Dir == Direction.LEFT)
            {
                if (newRotation.z < .7)
                    newRotation.z += .25f;
            }

            if (Dir == Direction.RIGHT)
            {
                if (newRotation.z > -.7)
                    newRotation.z -= .25f;
            }

            transform.localRotation = newRotation;
        }

        if (swing == Swing.UP)
        {
            if (Dir == Direction.LEFT)
            {
                if (newRotation.z > 0)
                {
                    newRotation.z -= .25f;
                    if (newRotation.z < 0)
                    {
                        newRotation.z = 0;
                        swing = Swing.NEUTRAL;
                    }
                    transform.localRotation = newRotation;
                }
            }

            if (Dir == Direction.RIGHT)
            {
                if (newRotation.z < 0)
                {
                    newRotation.z += .25f;
                    if (newRotation.z > 0)
                    {
                        newRotation.z = 0;
                        swing = Swing.NEUTRAL;
                    }
                    transform.localRotation = newRotation;
                }
            }
        }
    }

    void SuperHammer()
    {
        if (superHammerMode == true)
        {
            timer++;
            if (timer >= 1000)
                superHammerMode = false;
            Vector3 scale = new Vector3(4, 4, 2);
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = new Vector3(2.5f, 2.5f, 2);
            transform.localScale = scale;
            timer = 0;
            Destroy(SuperHammerIndic);
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "SuperHammer")
        {
            superHammerMode = true;
            Destroy(other.gameObject);
            SuperHammerIndic = Instantiate(SuperHammerPUPmini, superHammerIndicator, Quaternion.identity);
            audioSource.PlayOneShot(collect);
        }
    }

}
