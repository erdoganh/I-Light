using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMove : MonoBehaviour {

    public float xSpeed = -0.5f, ySpeed = 0.0f, destroyTime = 3f, stopTime = 1f;
    bool moveObjectBool = true;
    Vector3 currentPosition = new Vector3();

    void Start()
    {
        if (destroyTime > 0) Invoke("destroyObject", destroyTime);
    }

	// Update is called once per frame
	void Update () {

        if (moveObjectBool) moveObject();
	}

    void moveObject()
    {
        currentPosition = transform.position;

        currentPosition.x += xSpeed;
        currentPosition.y += ySpeed;

        transform.position = currentPosition;
        if (stopTime > 0) stopTime -= Time.deltaTime;
        else moveObjectBool = false;
    }

    void destroyObject()
    {
        Destroy(gameObject);

    }


}