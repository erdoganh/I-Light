using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOnOff : MonoBehaviour {

    float t = 0f;
    public float Speed = 0.01f;
    public Vector3 TargetRange = new Vector3();
    public float RepeatTime = 3f;
    Vector3 originalPosition = new Vector3();
    Vector3 targetPosition = new Vector3();

    bool MoveObjectUpBool = false;
    bool increaseT = true;

    Rigidbody r;

    AudioSource sound;

    void Start()
    {
        r = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        targetPosition = transform.position;
        targetPosition += TargetRange;
        InvokeRepeating("setMoveObjectUpBool", 0f, RepeatTime);

        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {

        if (MoveObjectUpBool) MoveObjectUp();

    }

    void MoveObjectUp()
    {

        r.MovePosition(Vector3.Lerp(originalPosition, targetPosition, t));

        if (increaseT) t += Speed;
        else t -= Speed/10f;

        if (t > 1.3) increaseT = false;
        else if (t < 0)
        {
            if (sound)
            {
                Invoke("PlaySound", 0.35f);
            }

            increaseT = true;
            t = 0;
            MoveObjectUpBool = false;

        }
    }

    void PlaySound()
    {
        sound.Stop();
        sound.Play();
    }

    void setMoveObjectUpBool()
    {
        
        MoveObjectUpBool = true;
    }
}
