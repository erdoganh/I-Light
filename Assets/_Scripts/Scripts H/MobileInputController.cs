using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputController : MonoBehaviour {


    public static bool isLeftPressed = false;
    public static bool isRightPressed = false;
    public static bool isUpPressed = false;

    [SerializeField] private Image[] arrows;
    [SerializeField] private float fadeTime = 7.5f;
    private Color destinationColor;
    private float timer = 0f;

    private void Start()
    {
        StartCoroutine(ArrowsFader());
        destinationColor = new Color(arrows[0].color.r, arrows[0].color.g, arrows[0].color.b, 0f) ;
    }

    IEnumerator ArrowsFader()
    {
        while(timer < fadeTime)
        {
            for (int i=0; i<arrows.Length; i++)
            {
                arrows[i].color = Color.Lerp(arrows[i].color, destinationColor, (timer / fadeTime));
            }

            timer = timer + 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    private void LateUpdate()
    {
        isUpPressed = false;
    }

    public void OnLeftArrowDown()
    {
        isLeftPressed = true;
    }

    public void OnLeftArrowUp()
    {
        isLeftPressed = false;
    }

    public void OnRightArrowDown()
    {
        isRightPressed = true;
    }

    public void OnRightArrowUp()
    {
        isRightPressed = false;
    }

    public void OnUpArrowPressed()
    {
        isUpPressed = true;
    }
}
