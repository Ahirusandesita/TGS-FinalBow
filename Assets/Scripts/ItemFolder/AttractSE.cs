// --------------------------------------------------------- 
// AttractSE.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class AttractSE : MonoBehaviour
{
    #region 変数一覧
    // 使用するAudioSource
    [SerializeField]
    AudioSource _myAudio = default;

    // 引き寄せた時のSE
    [SerializeField]
    AudioClip _attractSE = default;

    // 再生メソッドが呼ばれた回数　多重を防ぐために使用
    int _playCount = default;

    // 音割れを防ぐための再生間隔
    private const float DELAY_TIME = 0.05f;

    // 次の再生を行うまでの待機時間
    private float _replayTime = default;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド

    private void Start()
    {

    }

    private void Update()
    {
        // 前回の再生からの経過時間をカウントする
        _replayTime += Time.deltaTime;

        // 再生カウントがある かつ 再生間隔が経過している
        if(_playCount > 0 && _replayTime > DELAY_TIME)
        {
            // SEの再生
            _myAudio.PlayOneShot(_attractSE);

            // カウントを０に戻す
            _playCount = 0;
            _replayTime = 0;
        }
    }

    /// <summary>
    /// 引き寄せた時の音を呼ぶメソッド
    /// </summary>
    public void PlayAttractSE()
    {
        // 再生カウントを増やす
        _playCount++;
    }

 #endregion
}