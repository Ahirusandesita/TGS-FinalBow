// --------------------------------------------------------- 
// FPScount.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class FPScount : MonoBehaviour
{
    #region variable 

    int frameCount;
    float prevTime;
    float fps;

    public Text text;

    #endregion
    #region property
    #endregion
    #region method



    // ‰Šú‰»ˆ—
    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
    }
    // XVˆ—
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;

            #endregion
        }

        text.text = "FPS:" + fps;
    }
}