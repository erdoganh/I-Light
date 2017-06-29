using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float Speed = 1f;

    Vector3 currentTransform = new Vector3();

    Rigidbody r;
	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        r.velocity = Vector3.zero;
        currentTransform = transform.position;

        if (Input.GetKey("w"))
            currentTransform.y += 0.1f * Speed;

        if (Input.GetKey("s"))
            currentTransform.y -= 0.1f * Speed;

        if (Input.GetKey("a"))
            currentTransform.x -= 0.1f * Speed;

        if (Input.GetKey("d"))
            currentTransform.x += 0.1f * Speed;

        if (Input.GetKey("q"))
            currentTransform.z -= 0.1f * Speed;

        if (Input.GetKey("e"))
            currentTransform.z += 0.1f * Speed;

        r.MovePosition(currentTransform);
        //transform.position = currentTransform;
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.tag);
    }

    }
