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
    // �g�p����AudioSource
    [SerializeField]
    AudioSource _myAudio = default;

    // �����񂹂�����SE
    [SerializeField]
    AudioClip _attractSE = default;

    // �Đ����\�b�h���Ă΂ꂽ�񐔁@���d��h�����߂Ɏg�p
    int _playCount = default;

    // �������h�����߂̍Đ��Ԋu
    private const float DELAY_TIME = 0.05f;

    // ���̍Đ����s���܂ł̑ҋ@����
    private float _replayTime = default;
    #endregion

    #region �v���p�e�B

    #endregion

    #region ���\�b�h

    private void Start()
    {

    }

    private void Update()
    {
        // �O��̍Đ�����̌o�ߎ��Ԃ��J�E���g����
        _replayTime += Time.deltaTime;

        // �Đ��J�E���g������ ���� �Đ��Ԋu���o�߂��Ă���
        if(_playCount > 0 && _replayTime > DELAY_TIME)
        {
            // SE�̍Đ�
            _myAudio.PlayOneShot(_attractSE);

            // �J�E���g���O�ɖ߂�
            _playCount = 0;
            _replayTime = 0;
        }
    }

    /// <summary>
    /// �����񂹂����̉����Ăԃ��\�b�h
    /// </summary>
    public void PlayAttractSE()
    {
        // �Đ��J�E���g�𑝂₷
        _playCount++;
    }

 #endregion
}