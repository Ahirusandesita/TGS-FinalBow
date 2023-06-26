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
    #region �ϐ��ꗗ
    //�g�p����AudioSource
    AudioSource _myAudio = default;

    //�g�p����SE
    [SerializeField]
    AudioClip _attractSE = default;

    //
    int _playCount = default;

    #endregion

    #region �v���p�e�B

    #endregion

    #region ���\�b�h

    private void Start()
    {
        //�g�p����AudioSource���擾
        _myAudio = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //�Đ��J�E���g������΍Đ��@�J�E���g�Ŕ��肷�邱�Ƃœ����Đ��̉������h��
        if(_playCount > 0)
        {
            //SE�̍Đ�
            _myAudio.PlayOneShot(_attractSE);

            //�J�E���g���O�ɖ߂�
            _playCount = 0;
        }
    }

    /// <summary>
    /// �����񂹂����̉����Ăԃ��\�b�h
    /// </summary>
    public void PlayAttractSE()
    {
        //�Đ��J�E���g�𑝂₷
        _playCount++;
    }

 #endregion
}