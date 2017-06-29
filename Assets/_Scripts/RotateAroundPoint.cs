using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour {

    public float Speed = 200f, rotateRange = 8f;

    Vector3 currentPosition = new Vector3();
	// Use this for initialization
	void Start () {
        currentPosition = transform.position;
        currentPosition.y += rotateRange;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.forward != Vector3.forward)
            transform.RotateAround(currentPosition, -transform.forward, Speed * Time.deltaTime);
        else
            transform.RotateAround(currentPosition, transform.forward, Speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * Time.deltaTime*Speed);
    }
}
