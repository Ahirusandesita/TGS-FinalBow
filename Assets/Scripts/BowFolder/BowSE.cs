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

    [SerializeField] float _drawSE_MaxWaitTime = 0.7f;

    const int MAX_PERCENT = 1;

    const int SE_INDEX_DRAW = 0;

    const int SE_INDEX_ATTRACT = 1;

    const int SE_INDEX_SHOT = 2;

    AudioSource _audio = default;

    float[] cacheTimes = default;

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }


    public void CallDrawingSE(float drawPowerPercent)
    {
        if (LoopSEControlToPercent(drawPowerPercent,_drawSE_MaxWaitTime, SE_INDEX_DRAW))
        {
            _audio.PlayOneShot(_drawingClips[Random.Range(0, _drawingClips.Length)], drawPowerPercent);

        }
    }

    public void CallDrawStart()
    {
        _audio.PlayOneShot(_drawStartClips);
    }

    public void CallAttractSE(float attractPowerPercent)
    {
        if (LoopSEControlToConst(_attractClip.length, SE_INDEX_ATTRACT))
        {
            _audio.PlayOneShot(_attractClip,attractPowerPercent);

        }
    }

    public void CallShotSE()
    {
        _audio.PlayOneShot(_shotClip);
    }


    private bool LoopSEControlToPercent(float loopSpeed,float maxWaitTime, int indexSE)
    {
        if (loopSpeed > MAX_PERCENT)
        {
            loopSpeed = MAX_PERCENT;
        }

        if (cacheTimes[indexSE] + (maxWaitTime * (MAX_PERCENT - loopSpeed)) < Time.time)
        {
            cacheTimes[indexSE] = Time.time;

            return true;
        }
        return false;
    }

    private bool LoopSEControlToConst(float waitTime, int indexSE)
    {
        if (cacheTimes[indexSE] + waitTime < Time.time)
        {
            cacheTimes[indexSE] = Time.time;
            return true;
        }
        return false;
    }

    #endregion
}