using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

public void ExitGame()
    {
        print("EXIT");
        Application.Quit();
    }


    public void LoadLevel01()
    {
        Application.LoadLevel("Level 01");
    }
}
