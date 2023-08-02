// --------------------------------------------------------- 
// ShotWarp.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

public class ShotWarp : MonoBehaviour, IFCanTakeArrowButton
{
    #region variable 
    [SerializeField] Transform player;
    [SerializeField] bool moveWarp = false;
    [SerializeField] float moveTime = 0.1f;
    [SerializeField] bool canLight = false;
    [SerializeField] bool allLightToMove = true;
    [SerializeField] SpriteRenderer lightAlpha;
    [SerializeField] float fadeIn = 0.2f;
    [SerializeField] float stop = 0.3f;
    [SerializeField] float fadeOut = 0.2f;
    Color rgb = default;
    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    Func<float, IEnumerator> move = f => null;
    Func<float, float, float, IEnumerator> lightOut = (a,b,c) => null;
    Action transrate = () => { };
    Action lightCoroutine = () => { };
    Action moveCoroutine = () => { };
    
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        Init();
    }

    private void OnValidate()
    {
        Init();
    }
    private void Init()
    {

        moveCoroutine = () => player.position = transform.position;

        if (moveWarp)
        {
            move = new Func<float, IEnumerator>(WarpMove);
            allLightToMove = false;
            moveCoroutine = () => StartCoroutine(move(moveTime));
        }

        if (canLight)
        {
            lightOut = new Func<float, float, float, IEnumerator>(LightOut);
            lightCoroutine = () => StartCoroutine(lightOut(fadeIn,stop,fadeOut));
        }

        if (allLightToMove)
        {
            transrate = () => player.position = transform.position;
        }
        rgb = lightAlpha.color;
        lightAlpha.color = new Color(rgb.r, rgb.g, rgb.b, 0);
        
    }

    public void ButtonPush()
    {
        print("aaa");
        lightCoroutine();
        moveCoroutine();
    }

    IEnumerator WarpMove(float time)
    {
        float distance = Vector3.Distance(player.position, transform.position);
        float speed = distance /time;
        Vector3 moveVec = (transform.position - player.position).normalized;
        Vector3 move = speed * Time.deltaTime * moveVec;
        float moved = 0f;
        while (true)
        {
            yield return null;
            player.transform.Translate(move,Space.World);
            moved += move.magnitude;
            if(moved > distance)
            {
                //player.position = transform.position;
                break;
            }

        }
    }

    IEnumerator LightOut(float first,float blind,float fix)
    {
        float time = 0f;
        float alpha = lightAlpha.color.a;
        while (true)
        {
            yield return null;
            if (time < first)
            {
                alpha += (1 / first) * Time.deltaTime;
            }
            else if (time < first + blind)
            {
                alpha = 1;
                transrate();
            }
            else if (time < first + blind + fix)
            {
                alpha -= (1 / blind) * Time.deltaTime;
            }
            else
            {
                alpha = 0f;
            }

            lightAlpha.color = new Color(rgb.r, rgb.g, rgb.b, alpha);
            if(alpha <= 0)
            {
                break;
            }
            time += Time.deltaTime;

        }
    }
    #endregion
}