using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IFCanvasEvent
{
    void EnableStart();

    void DisableStart();
}
[RequireComponent(typeof(Image))]
public class TakeDamageUI : MonoBehaviour, IFCanvasEvent
{
    Image myImage = default;
    WaitForSeconds wait = new WaitForSeconds(stopTime);
    Coroutine coroutine = default;
    Color myColor = default;

    const float enableTime = 0.1f;
    const float stopTime = 0.2f;
    const float disableTime = 0.4f;

    public void DisableStart()
    {
        myImage = GetComponent<Image>();
        myColor = myImage.color;
        Color alphaZero = myColor;
        alphaZero.a = 0;
        myImage.color = alphaZero;
    }

    public void EnableStart()
    {


        if(coroutine is not null)
        StopCoroutine(coroutine);

        coroutine = StartCoroutine(UIAnimation());
    }

    IEnumerator UIAnimation()
    {
        
        float firstAddAlpha = 1 / enableTime;

        float secondDisAlpha = 1 / disableTime;

        Color cacheColor = myImage.color;

        while (myImage.color.a < 1)
        {
            
            cacheColor.a += firstAddAlpha * Time.deltaTime;
            myImage.color = cacheColor;
            yield return null;
        }

        yield return wait;

        while (myImage.color.a > 0)
        {
            
            cacheColor.a -= secondDisAlpha * Time.deltaTime;
            myImage.color = cacheColor;
            yield return null;
        }
    }
}
