// --------------------------------------------------------- 
// BowSE.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

public interface IFBowSE_CallToBow
{
    void CallDrawingSE(float drawPowerPercent);

    void CallDrawStart();

    void CallShotSE();

    void CallAttractSE(float attractPowerPercent);

    void CallAttractStartSE();

    void StopAttractSE();

}

interface IFBowSE_CallToAttract
{
    void CallItemGetSE();
}

[RequireComponent(typeof(AudioSource))]
public class BowSE : MonoBehaviour,IFBowSE_CallToBow,IFBowSE_CallToAttract
{

    #region variable 

    [SerializeField] AudioClip[] _drawingClips = default;

    [SerializeField] AudioClip _drawStartClips = default;

    [SerializeField] AudioClip _attractClip = default;

    [SerializeField] AudioClip _attractStartClip = default;

    [SerializeField] AudioClip _shotClip = default;

    [SerializeField] AudioClip _getItemClip = default;

    [SerializeField] AudioSource _attractSource = default;

    [SerializeField] float _drawSE_MaxWaitTime = 0.7f;

    const int MAX_PERCENT = 1;

    const int SE_INDEX_DRAW = 0;

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

        _attractSource.clip = _attractClip;
    }


    public void CallDrawingSE(float drawPowerPercent)
    {
        if (LoopSEControlToPercent(drawPowerPercent, _drawSE_MaxWaitTime, SE_INDEX_DRAW))
        {
            _myAudio.PlayOneShot(_drawingClips[Random.Range(0, _drawingClips.Length)], drawPowerPercent);

        }
    }

    public void CallDrawStart()
    {
        X_Debug.LogError("start");
        _myAudio.PlayOneShot(_drawStartClips);
        X_Debug.LogError("end");
    }

    public void CallAttractSE(float attractPowerPercent)
    {
        _attractSource.volume = attractPowerPercent;

        if (!_attractSource.isPlaying)
        {
            _attractSource.Play();
        }
    }

    public void CallAttractStartSE()
    {
        _attractSource.PlayOneShot(_attractStartClip);
    }

    public void StopAttractSE()
    {
        _attractSource.Stop();
    }

    public void CallShotSE()
    {
        _myAudio.PlayOneShot(_shotClip);
    }

    public void CallItemGetSE()
    {
        _myAudio.PlayOneShot(_getItemClip);
    }

    private bool LoopSEControlToPercent(float loopSpeed, float maxWaitTime, int indexSE)
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