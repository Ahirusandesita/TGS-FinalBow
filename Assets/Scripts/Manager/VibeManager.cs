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
    /// ����̃o�C�u
    /// </summary>
    /// <param name="frequency">�U��0-1</param>
    /// <param name="amplitude">�U����0-1</param>
    public void LeftStartVibe(float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LHand);
    }

    /// <summary>
    /// �E��̃o�C�u
    /// </summary>
    /// <param name="frequency">�U��0-1</param>
    /// <param name="amplitude">�U����0-1</param>
    public void RightStartVibe(float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RHand);
    }

    public void TwinHandsVibe(float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.Hands);
    }
    /// <summary>
    /// ����̃o�C�u�I��
    /// </summary>
    public void LeftEndVibe()
    {
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LHand);
    }

    /// <summary>
    /// �E��̃o�C�u�̏I��
    /// </summary>
    public void RightEndVibe()
    {
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RHand);
    }

    /// <summary>
    /// �I�����ԕt���E��̐U��
    /// </summary>
    /// <param name="frequency">�U��0-1</param>
    /// <param name="amplitude">�U����0-1</param>
    /// <param name="endTime">�I������</param>
    public void RightVibeSetEnd(float frequency, float amplitude, float endTime)
    {
        StartCoroutine(StartVibeSetEnd(frequency, amplitude, endTime, OVRInput.Controller.RHand));
    }

    /// <summary>
    /// �I�����ԕt������̐U��
    /// </summary>
    /// <param name="frequency">�U��0-1</param>
    /// <param name="amplitude">�U����0-1</param>
    /// <param name="endTime">�I������</param>
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