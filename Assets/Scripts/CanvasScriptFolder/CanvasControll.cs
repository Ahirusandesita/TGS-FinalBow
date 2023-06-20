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
        //RecoveryImage��DamageImage���Q�b�g�R���|�[�l���g���Ĕ�\���ɂ���
        _myTransform = this.transform;

        for (int i = 0; i < _lifeImages.Length; i++)
        {
            _lifeImages[i] = _myTransform.GetChild(i).GetComponent<Image>();
            _lifeImages[i].enabled = _isLifeImage;
        }
    }

    /// <summary>
    /// ���C�t�񕜂̉��o����
    /// </summary>
    public void LifeRecovery()
    {
        StartCoroutine(LifeStaging(LIFE_RECOVERY));
    }

    /// <summary>
    /// ���C�t�_���[�W�̉��o����
    /// </summary>
    public void LifeDamage()
    {
        StartCoroutine(LifeStaging(LIFE_DAMAGE));
    }

    private IEnumerator LifeStaging(int index)
    {
        //RecoveryImage��DamageImage�̓_�ŏ���
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
        //�_�ŉ񐔂���񐔂ł��Ō�͕K��Image���\���ɂȂ�悤�ɂ���
        _isLifeImage = false;
        _lifeImages[index].enabled = _isLifeImage;
    }



    #endregion
}