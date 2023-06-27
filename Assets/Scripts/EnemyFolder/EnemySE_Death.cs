// --------------------------------------------------------- 
// EnemySE_Death.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class EnemySE_Death : MonoBehaviour
{
    #region 変数一覧

    // 
    [SerializeField]
    private GameObject _AudioObject = default;

    // 使用するAudioSource
    private AudioSource _myAudio = default;

    // 敵が死んだときのSE
    [SerializeField]
    public AudioClip _deathSE = default;

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

    private void Start ()
    {
        _myAudio = _AudioObject.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 前回の再生からの経過時間をカウントする
        _replayTime += Time.deltaTime;

        // 再生カウントがある かつ 再生間隔が経過している
        if (_playCount > 0 && _replayTime > DELAY_TIME)
        {
            // SEの再生
            _myAudio.PlayOneShot(_deathSE);

            // カウントを０に戻す
            _playCount = 0;
            _replayTime = 0;
        }
    }

    /// <summary>
    /// 敵が死んだときのSEを再生するメソッド
    /// </summary>
    public void PlayEnemyDeathSE(Vector3 playPosition)
    {
        // 代入前に実行されるのを防ぐため
        if(_myAudio != null)
        {
            // 再生位置を変更
            _AudioObject.transform.position = playPosition;

            // 再生カウントを加算
            _playCount ++;
        }
        else
        {
            // 何もせずに返す
            return;
        }
    }

 #endregion
}