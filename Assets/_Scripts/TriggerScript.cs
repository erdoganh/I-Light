using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public SpeedMove[] SM;
    private bool soundPlayed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio && !soundPlayed)
        {
            soundPlayed = true;
            audio.Stop();
            audio.Play();
        }

        foreach(SpeedMove sm in SM)
        {
            sm.enabled = true;
        }
        
    }
}
