using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLight : MonoBehaviour {

    Light PL;

    float CurrentLightRange = 1, TempLightRange = 8, Speed = 0.01f;

    public float t=0;

    bool increaseT = true;

    // Use this for initialization
    void Start()
    {
        PL = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        PL.intensity = Mathf.Lerp(CurrentLightRange, TempLightRange, t);

        if (increaseT) t += Speed;
        else t -= Speed;

        if (t > 1) increaseT = false;
        else if (t < 0)
        {
            increaseT = true;
            t = 0;
        }
    }
}

