using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishController : MonoBehaviour {


    public string nextSceneName = "Level ";

    public static bool loadSameLevel = false;

    public Image fadeImage;
    public Color fadeColor;

    private bool isTriggered = false;

    public float switchLevelTime = 2f;
    private float timer = 0f;
    private bool isStart = true;

    private void Awake()
    {
        fadeImage.enabled = true;
        fadeImage.color = Color.black;
        //StartCoroutine(StartLevel());
    }


    private void Update()
    {
        if (loadSameLevel)
        {
            timer = timer + Time.deltaTime;
            if (timer > 4f)
            {
                loadSameLevel = false;
                Application.LoadLevel(Application.loadedLevel);
            }
            else
                fadeImage.color = Color.Lerp(fadeImage.color, Color.black, timer / 4);
        }

        if (isStart)
        {
            timer = timer + Time.deltaTime;
            if (timer > switchLevelTime)
            {
                fadeImage.enabled = false;
                isStart = false;
            }
            else
                fadeImage.color = Color.Lerp(fadeImage.color, fadeColor, timer / switchLevelTime);
        }

        if (isTriggered)
        {
            timer = timer + Time.deltaTime;
            if (timer > switchLevelTime)
            {
                Application.LoadLevel(nextSceneName);
            }
            else
                fadeImage.color = Color.Lerp(fadeImage.color, Color.black, timer / switchLevelTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (!isTriggered)
            {
                timer = 0f;
                isTriggered = true;
                fadeImage.enabled = true;
            }
        }
    }

    IEnumerator StartLevel()
    {
        while (fadeImage.color.a > 0.05f)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - 0.01f);
            yield return new WaitForSeconds(0.1f);    
        }
        fadeImage.enabled = false;
        yield return null;
    }

}
