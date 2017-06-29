using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyScript : MonoBehaviour {

    float speed = 0.001f, t=0f;

    Vector3 TargetPosition = new Vector3();

	// Use this for initialization
	void Start () {
        getTargetPosition();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, t);

        t += speed;

        if (t > 1) Destroy(gameObject);
    }

    void getTargetPosition()
    {

        TargetPosition = new Vector3(transform.position.x + (Random.value-0.5f)*50, transform.position.y + (Random.value-0.5f)*50, 0);
    }
}

