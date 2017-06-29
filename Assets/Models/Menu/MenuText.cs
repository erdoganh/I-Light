using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuText : MonoBehaviour {


    float CurrentZ = 1, TempZ = 8, t = 0, Speed = 0.01f;

    new Vector3 TempPosition = new Vector3();

    bool increaseT = true;

    MenuLight ML;

    // Use this for initialization
    void Start()
    {
        ML = GameObject.Find("Point light").GetComponent<MenuLight>();
        TempPosition = transform.position;

        CurrentZ = TempPosition.z;
        TempZ = CurrentZ / 2;
    }

    // Update is called once per frame
    void Update()
    {
        t = ML.t;
        TempPosition.z = Mathf.Lerp(CurrentZ, TempZ, t);

        if (increaseT) t += Speed;
        else t -= Speed;

        if (t > 1) increaseT = false;
        else if (t < 0)
        {
            increaseT = true;
            t = 0;
        }

        transform.position = TempPosition;
    }
}
