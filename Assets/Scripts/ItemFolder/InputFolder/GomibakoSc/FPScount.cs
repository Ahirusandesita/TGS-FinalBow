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



    // 初期化処理
    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
    }
    // 更新処理
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