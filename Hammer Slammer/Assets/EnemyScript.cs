using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip hurt;

    public float enemySpeed;
    public bool commence;

    // Use this for initialization
    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        MoveEnemy();
	}

    

    void MoveEnemy()
    {
        Vector3 enemyPosition = transform.position;
        enemyPosition.x += Time.deltaTime * enemySpeed;
        transform.position = enemyPosition;

        if (enemyPosition.y <= -7)
        {
            Destroy(gameObject);
        }
    }
}
