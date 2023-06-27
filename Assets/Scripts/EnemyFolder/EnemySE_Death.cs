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
    #region �ϐ��ꗗ

    // 
    [SerializeField]
    private GameObject _AudioObject = default;

    // �g�p����AudioSource
    private AudioSource _myAudio = default;

    // �G�����񂾂Ƃ���SE
    [SerializeField]
    public AudioClip _deathSE = default;

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

    private void Start ()
    {
        _myAudio = _AudioObject.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // �O��̍Đ�����̌o�ߎ��Ԃ��J�E���g����
        _replayTime += Time.deltaTime;

        // �Đ��J�E���g������ ���� �Đ��Ԋu���o�߂��Ă���
        if (_playCount > 0 && _replayTime > DELAY_TIME)
        {
            // SE�̍Đ�
            _myAudio.PlayOneShot(_deathSE);

            // �J�E���g���O�ɖ߂�
            _playCount = 0;
            _replayTime = 0;
        }
    }

    /// <summary>
    /// �G�����񂾂Ƃ���SE���Đ����郁�\�b�h
    /// </summary>
    public void PlayEnemyDeathSE(Vector3 playPosition)
    {
        // ����O�Ɏ��s�����̂�h������
        if(_myAudio != null)
        {
            // �Đ��ʒu��ύX
            _AudioObject.transform.position = playPosition;

            // �Đ��J�E���g�����Z
            _playCount ++;
        }
        else
        {
            // ���������ɕԂ�
            return;
        }
    }

 #endregion
}