using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlamZoneScript : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip collect;
    public AudioClip spawn;
    public AudioClip destroy;

    public Text scoreBox;

    public GameObject ScoreBallPrefab;
    public GameObject SuperHammerPUP;
    public GameObject InvinciblePUP;

    GameObject SuperHammerPowerUp;
    GameObject InvinciblePowerUp;

    public Transform PUPDropper;

    private bool spawnPowerUp = false;
    
    private int oldScore = 0;
    private bool powerUpAvailable = false;
    private int powerUpConsumed = 0;
    public int timer = 0;
    private int num;
    private int randNum;

    bool blocking = false;
    float rotation;
    bool startGroundCount = false;
    int groundCount;
    Vector3 blockVector = new Vector3(0, Time.deltaTime * 12, 0);

    public int score = 0;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        scoreBox.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        BlockCheck();
        AddScore();
        IsPowerUp();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (blocking == false)
            {
                Destroy(other.gameObject);
                audioSource.PlayOneShot(destroy);
                score++;
                scoreBox.text = (score * 10).ToString();
            }
        }
    }

    void BlockCheck()
    {
        rotation = transform.rotation.z;

        if (rotation != 0)
            startGroundCount = true;

        if (startGroundCount == true)
            groundCount++;
        if (groundCount == 10 && powerUpConsumed == 0)
            blocking = true;

        if (transform.rotation.z == 0)
        {
            blocking = false;
            groundCount = 0;
        }
    }

    void IsPowerUp()
    {
        if (powerUpConsumed >= 1)
        {
            timer++;
            if (timer >= 1000)
            {
                powerUpConsumed = 0;
                timer = 0;
            }
        }
    }

    void AddScore()
    {
        if (score % 20 == 0 && oldScore != score)
        {
            oldScore = score;
            randNum = Random.Range(0, 3);
            if (powerUpAvailable == false)
            {
                spawnPowerUp = true;
                num = randNum;
            }
        }

        SuperHammer(num);
        Invincible(num);
    }

    void SuperHammer(int num)
    {
        if (num == 1)
        {
            if (spawnPowerUp == true && powerUpAvailable == false && powerUpConsumed == 0)
            {
                SuperHammerPowerUp = Instantiate(SuperHammerPUP, PUPDropper.position, Quaternion.identity);
                spawnPowerUp = false;
                powerUpAvailable = true;
                audioSource.PlayOneShot(spawn);
            }
            if (!SuperHammerPowerUp)
            {
                powerUpAvailable = false;
                powerUpConsumed++;
                num = 0;
            }
        }
    }

    void Invincible(int num)
    {
        if (num == 2)
        {
            if (spawnPowerUp == true && powerUpAvailable == false && powerUpConsumed == 0)
            {
                InvinciblePowerUp = Instantiate(InvinciblePUP, PUPDropper.position, Quaternion.identity);
                spawnPowerUp = false;
                powerUpAvailable = true;
                audioSource.PlayOneShot(spawn);
            }
            if (!InvinciblePowerUp)
            {
                powerUpAvailable = false;
                powerUpConsumed++;
                num = 0;
            }
        }
    }
}
