// --------------------------------------------------------- 
// CanvasControll.cs 
// 
// CreateDay: 2023/06/18
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

interface ICanvasControll
{
    void LifeRecovery();
    void LifeDamage();
}

public class CanvasControll : MonoBehaviour,ICanvasControll
{
    #region variable 
    private int _chengeImageValue = 6;

    private Transform _myTransform;
    private Image[] _lifeImages = new Image[2];

    private bool _isLifeImage = false;
    private const int LIFE_RECOVERY = 0;
    private const int LIFE_DAMAGE = 1;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        //RecoveryImageとDamageImageをゲットコンポーネントして非表示にする
        _myTransform = this.transform;

        for (int i = 0; i < _lifeImages.Length; i++)
        {
            _lifeImages[i] = _myTransform.GetChild(i).GetComponent<Image>();
            _lifeImages[i].enabled = _isLifeImage;
        }
    }

    /// <summary>
    /// ライフ回復の演出処理
    /// </summary>
    public void LifeRecovery()
    {
        StartCoroutine(LifeStaging(LIFE_RECOVERY));
    }

    /// <summary>
    /// ライフダメージの演出処理
    /// </summary>
    public void LifeDamage()
    {
        StartCoroutine(LifeStaging(LIFE_DAMAGE));
    }

    private IEnumerator LifeStaging(int index)
    {
        //RecoveryImageとDamageImageの点滅処理
        for (int i = 0; i < _chengeImageValue; i++)
        {
            yield return new WaitForSeconds(0.2f);

            if (_isLifeImage)
            {
                _isLifeImage = false;
            }
            else if (!_isLifeImage)
            {
                _isLifeImage = true;
            }

            _lifeImages[index].enabled = _isLifeImage;

        }
        //点滅回数が奇数回数でも最後は必ずImageを非表示になるようにする
        _isLifeImage = false;
        _lifeImages[index].enabled = _isLifeImage;
    }



    #endregion
}