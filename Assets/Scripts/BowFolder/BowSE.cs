// --------------------------------------------------------- 
// BowSE.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class BowSE : MonoBehaviour
{

    #region variable 

    [SerializeField] AudioClip[] _drawingClips = default;

    [SerializeField] AudioClip _drawStartClips = default;

    [SerializeField] AudioClip _attractClip = default;

    [SerializeField] AudioClip _shotClip = default;

    [SerializeField] AudioSource _attractSource = default;

    [SerializeField] float _drawSE_MaxWaitTime = 0.7f;

    const int MAX_PERCENT = 1;

    const int SE_INDEX_DRAW = 0;

    const int SE_INDEX_ATTRACT = 1;

    const int SE_INDEX_SHOT = 2;

    const float SE_LIMIT_LOOP_SPEED = 0.2f;

    AudioSource _myAudio = default;

    float[] _cacheTimes = new float[4];

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
    }


    public void CallDrawingSE(float drawPowerPercent)
    {
        if (LoopSEControlToPercent(drawPowerPercent,_drawSE_MaxWaitTime, SE_INDEX_DRAW))
        {
            _myAudio.PlayOneShot(_drawingClips[Random.Range(0, _drawingClips.Length)], drawPowerPercent);

        }
    }

    public void CallDrawStart()
    {
        _myAudio.PlayOneShot(_drawStartClips);
    }

    public void CallAttractSE(float attractPowerPercent)
    {
        if (LoopSEControlToConst(_attractClip.length, SE_INDEX_ATTRACT))
        {
            _attractSource.PlayOneShot(_attractClip,attractPowerPercent);

        }
    }

    public void CallShotSE()
    {
        _myAudio.PlayOneShot(_shotClip);
    }


    private bool LoopSEControlToPercent(float loopSpeed,float maxWaitTime, int indexSE)
    {
        if (loopSpeed > MAX_PERCENT)
        {
            loopSpeed = MAX_PERCENT;
        }

        float waitTimePercent = MAX_PERCENT - loopSpeed;

        if (waitTimePercent < SE_LIMIT_LOOP_SPEED)
        {
            waitTimePercent = SE_LIMIT_LOOP_SPEED;
        }

        if (_cacheTimes[indexSE] + (maxWaitTime * waitTimePercent) < Time.time)
        {
            _cacheTimes[indexSE] = Time.time;

            return true;
        }
        return false;
    }

    private bool LoopSEControlToConst(float waitTime, int indexSE)
    {
        if (_cacheTimes[indexSE] + waitTime < Time.time)
        {
            _cacheTimes[indexSE] = Time.time;
            return true;
        }
        return false;
    }

    #endregion
}