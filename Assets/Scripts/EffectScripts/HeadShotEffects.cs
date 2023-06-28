// --------------------------------------------------------- 
// HeadShotEffects.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

interface IHeadShotEffects
{
    void PlayHeadShotEffect(Vector3 position);
}
public class HeadShotEffects : MonoBehaviour, IHeadShotEffects
{
    #region variable 
    [SerializeField] AudioClip _headShotSE = default;

    [SerializeField] GameObject _particle = default;

    AudioSource _mySource = default;
    #endregion
    #region property
    #endregion
    #region method



    private void Start()
    {
        _mySource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ヘッドショット時のSE、エフェクト
    /// </summary>
    public void PlayHeadShotEffect(Vector3 enemyHeadPosition)
    {
        X_Debug.Log("aaa");
        _mySource.PlayOneShot(_headShotSE);

        Instantiate(_particle, enemyHeadPosition, Quaternion.identity);
    }

    #endregion
}