using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    Quaternion newRotation;
    public int count = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        newRotation = transform.rotation;
        newRotation.z += .001f;
        transform.rotation = newRotation;
        count++;
    }
}
