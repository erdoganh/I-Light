using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyControllerScript : MonoBehaviour {

    public GameObject FireFlyPrefab;

	// Use this for initialization
	void Start () {
        InvokeRepeating("createFireFly", 0f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void createFireFly()
    {
        Instantiate(FireFlyPrefab, getRandomPosition(),Quaternion.identity);
    }

    Vector3 getRandomPosition()
    {
        return new Vector3(transform.position.x + (Random.value - 0.5f) * 5, transform.position.y + (Random.value - 0.5f) * 5, 0);
    }
}
