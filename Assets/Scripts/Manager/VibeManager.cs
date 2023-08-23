// --------------------------------------------------------- 
// VibeManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public class VibeManager : MonoBehaviour
{
    #region variable 
    
    #endregion
    #region property
    #endregion
    #region method

    /// <summary>
    /// 左手のバイブ
    /// </summary>
    /// <param name="frequency">振幅0-1</param>
    /// <param name="amplitude">振動数0-1</param>
    public void LeftStartVibe(float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LHand);
    }

    /// <summary>
    /// 右手のバイブ
    /// </summary>
    /// <param name="frequency">振幅0-1</param>
    /// <param name="amplitude">振動数0-1</param>
    public void RightStartVibe(float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RHand);
    }

    public void TwinHandsVibe(float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.Hands);
    }
    /// <summary>
    /// 左手のバイブ終了
    /// </summary>
    public void LeftEndVibe()
    {
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LHand);
    }

    /// <summary>
    /// 右手のバイブの終了
    /// </summary>
    public void RightEndVibe()
    {
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RHand);
    }

    /// <summary>
    /// 終了時間付き右手の振動
    /// </summary>
    /// <param name="frequency">振幅0-1</param>
    /// <param name="amplitude">振動数0-1</param>
    /// <param name="endTime">終了時間</param>
    public void RightVibeSetEnd(float frequency, float amplitude, float endTime)
    {
        StartCoroutine(StartVibeSetEnd(frequency, amplitude, endTime, OVRInput.Controller.RHand));
    }

    /// <summary>
    /// 終了時間付き左手の振動
    /// </summary>
    /// <param name="frequency">振幅0-1</param>
    /// <param name="amplitude">振動数0-1</param>
    /// <param name="endTime">終了時間</param>
    public void LeftVibeSetEnd(float frequency, float amplitude, float endTime)
    {
        StartCoroutine(StartVibeSetEnd(frequency, amplitude, endTime, OVRInput.Controller.LHand));
    }

    private IEnumerator StartVibeSetEnd(float frequency, float amplitude, float endTime, OVRInput.Controller controller)
    {
        float startTime = Time.time;

        while (Time.time < startTime + endTime)
        {
            OVRInput.SetControllerVibration(frequency, amplitude, controller);
            yield return null;
        }

        OVRInput.SetControllerVibration(0f, 0f, controller);

    }
    #endregion
}