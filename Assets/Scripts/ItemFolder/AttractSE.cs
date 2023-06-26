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
    //使用するAudioSource
    AudioSource _myAudio = default;

    //使用するSE
    [SerializeField]
    AudioClip _attractSE = default;

    //
    int _playCount = default;

    #endregion

    #region プロパティ

    #endregion

    #region メソッド

    private void Start()
    {
        //使用するAudioSourceを取得
        _myAudio = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //再生カウントがあれば再生　カウントで判定することで同時再生の音割れを防ぐ
        if(_playCount > 0)
        {
            //SEの再生
            _myAudio.PlayOneShot(_attractSE);

            //カウントを０に戻す
            _playCount = 0;
        }
    }

    /// <summary>
    /// 引き寄せた時の音を呼ぶメソッド
    /// </summary>
    public void PlayAttractSE()
    {
        //再生カウントを増やす
        _playCount++;
    }

 #endregion
}