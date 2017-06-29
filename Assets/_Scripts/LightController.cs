using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public bool isFireFly = false;
    Light PL;

    float CurrentLightRange = 0, TempLightRange = 0, t = 0, Speed = 0.01f;

    bool increaseT = true;

	// Use this for initialization
	void Start () {
        PL = GetComponent<Light>();
        SetRangeValues(PL.range);
	}

    // Update is called once per frame
    void Update()
    {

        PL.range = Mathf.Lerp(CurrentLightRange, TempLightRange, t);

        if (increaseT) t += Speed;
        else t -= Speed;

        if (t > 1) increaseT = false;
        else if (t < 0)
        {
            increaseT = true;
            t = 0;
        }
    }

    void SetRangeValues(float LightRange)
    {
        CurrentLightRange = LightRange;
        TempLightRange = CurrentLightRange + CurrentLightRange / 5f;

        if (isFireFly)
        {
            CurrentLightRange = 0;
            TempLightRange = 1f;
        }
    }
}
