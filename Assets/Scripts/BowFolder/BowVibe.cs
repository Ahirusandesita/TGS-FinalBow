// --------------------------------------------------------- 
// BowVibe.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

interface IFBowVibe
{
    void StartShotVibe(float power);

    void SetLeftShotAction();

    void SetRightShotAction();

    void StartDrawVibe(float power);

    void EndDrawVibe();

}
public class BowVibe : MonoBehaviour, IFBowVibe
{
    #region variable 

    [Tooltip("�V���b�g���̐U��")]
    [SerializeField] CreateAnimationCurve _shotFrequencyCurve = default;


    [Tooltip("�V���b�g���̐U����")]
    [SerializeField] CreateAnimationCurve _shotAmplitudeCurve = default;

    [SerializeField] TagObject _vibeTagName = default;

    private Action<float, float> _vibeAction = default;

    private AnimationCurve _shotFrequency = default;

    private AnimationCurve _shotAmplitude = default;

    private VibeManager _vibeManager = default;

    #endregion
    #region property

    #endregion
    #region method

    private void Awake()
    {
        _shotAmplitude = _shotAmplitudeCurve.Curve;

        _shotFrequency = _shotFrequencyCurve.Curve;

        _vibeManager = GameObject.FindWithTag(_vibeTagName.TagName).GetComponent<VibeManager>();
    }

    public void StartShotVibe(float power)
    {

        try
        {
            StartCoroutine(ShotCoroutine(power));

        }
        catch (ArgumentOutOfRangeException)
        {
            X_Debug.LogError("Curve���ݒ肳��Ă��܂���");
        }
    }

    public void SetLeftShotAction()
    {
        try
        {
            _vibeAction = new Action<float, float>(_vibeManager.LeftStartVibe);
        }
        catch (ArgumentOutOfRangeException)
        {
            X_Debug.LogError("����VibeManager���Ȃ�");
        }
    }

    public void SetRightShotAction()
    {
        try
        {
            _vibeAction = new Action<float, float>(_vibeManager.RightStartVibe);
        }
        catch (ArgumentOutOfRangeException)
        {
            X_Debug.LogError("����VibeManager���Ȃ�");
        }
    }

    public void StartDrawVibe(float power)
    {
        // �o�C�u��߂�
        power = power / 2;
        
        _vibeManager.TwinHandsVibe(power, power);
    }

    public void EndDrawVibe()
    {
        _vibeManager.TwinHandsVibe(0, 0);
    }

    private IEnumerator ShotCoroutine(float power)
    {
        float startTime = Time.time;

        float freq = 0f;

        float amp = 0f;

        float endTime = startTime;

        // �I�����Ԍ��߂�(�x����)
        if (_shotAmplitude.keys[_shotAmplitude.length - 1].time >
            _shotFrequency.keys[_shotFrequency.length - 1].time)
        {
            endTime = startTime + _shotAmplitude.keys[_shotAmplitude.length - 1].time;
        }
        else
        {
            endTime = startTime + _shotFrequency.keys[_shotFrequency.length - 1].time;
        }

        while (Time.time < endTime)
        {
            amp = _shotAmplitude.Evaluate(Time.time - startTime)* power;

            freq = _shotFrequency.Evaluate(Time.time - startTime) * power;

            try
            {
                // ���݂̎��Ԃ̐U��������
                _vibeAction?.Invoke(freq, amp);
            }
            catch (NullReferenceException)
            {
                X_Debug.LogError("�o�C�u�̃A�N�V�������Z�b�g����Ă��܂���");
            }

            yield return null;
        }
    }
    #endregion
}