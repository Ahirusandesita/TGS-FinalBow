// --------------------------------------------------------- 
// BossAttack.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�X�̍U��
/// </summary>
public class BossAttack : EnemyAttack
{
    #region �ϐ�
    [Header("�΂̋ʍU���̎G���̏����ʒu"), Tooltip("��������G���̃X�|�i�[��o�^")]
    public List<Transform> _FG_birdSpawnPlaces = new List<Transform>();

    [Header("�΂̋ʍU���̏o���ʒu"), Tooltip("FireGatling�̃X�|�i�[��Transform���")]
    public Transform _FG_spawnPlace = default;


    [Tooltip("�U���Ԃ̋x�e�b��")]
    private WaitForSeconds _FG_attackIntervalWait = default;

    [Tooltip("�A���U���̊Ԋu")]
    private WaitForSeconds _FG_attackRequiredWait = default;

    [Tooltip("��������/�����G���̐�")]
    private int _FG_numberOfBirds = default;

    [Header("�p�����[�^"), SerializeField, Tooltip("�΂̋ʍU���̍U����")]
    private int _FG_numberOfAttack = 5;

    // �f�o�b�O�p������
    [SerializeField, Tooltip("�U���Ԃ̋x�e�b��")]
    private float _FG_attackIntervalTime = 1.5f;

    [SerializeField, Tooltip("�U��1�Z�b�g�̏��v����")]
    private float _FG_attackRequiredTime = 3f;


    [Tooltip("�΂̋ʂ̍ő吔")]
    private int MAX_FIRE_BALL = 9;
    #endregion


    protected override void Start()
    {
        // PoolSystem��GetComponent
        base.Start();

        // �u���������G���̐��v�ɁA�o�^�����X�|�i�[���X�g�̗v�f������
        _FG_numberOfBirds = _FG_birdSpawnPlaces.Count;

        // �ݒ肵���x�e�b����WaitForSeconds�ɑ��
        _FG_attackIntervalWait = new WaitForSeconds(_FG_attackIntervalTime);

        // �ݒ肵���U���̏��v���Ԃ���A��񂲂Ƃ̒l���v�Z����WaitForSeconds�ɑ��
        _FG_attackRequiredWait = new WaitForSeconds(_FG_attackRequiredTime / _FG_numberOfAttack);
    }


    /// <summary>
    /// �΂̋ʍU���̂��߂̓���ȎG������������
    /// </summary>
    public void SpawnBirdsForFireGatling()
    {
        BirdStats birdStats;

        // �G������������
        for (int i = 0; i < _FG_numberOfBirds; i++)
        {
            // �v�[�����璹�����o���A�R���|�[�l���g���擾
            birdStats = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _FG_birdSpawnPlaces[i].position, Quaternion.Euler(Vector3.up * 180f)).GetComponent<BirdStats>();

            // ���o��������bool�ϐ���true�ɂ���
            birdStats.IsSummmon = true;

            // ���o�������̃f���Q�[�g�ϐ��ɁA�f�N�������g���郁�\�b�h��o�^
            birdStats._onDeathBird = DecrementFG_NumberOfBird;
        }

        // �΂̋ʍU�����X�^�[�g������
        StartCoroutine(FireGatling());
    }

    /// <summary>
    /// �{�X�F�΂̋ʍU���iFG�j
    /// <para>���������F�c��HP40���ȉ�</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireGatling()
    {
        // ��x�ɃX�|�[������΂̋ʂ̐�
        int numberOfFireBall;

        // ���������G����0�ɂȂ�܂ōU���𑱂���
        while (_FG_numberOfBirds > 0)
        {
            // �΂̋ʂ̐���ݒ�
            numberOfFireBall = _FG_numberOfBirds;

            // �΂̋ʂ̍ő吔�ɃN�����v
            if (numberOfFireBall > MAX_FIRE_BALL)
            {
                numberOfFireBall = MAX_FIRE_BALL;
            }

            // �΂̋ʂ̐���������������1���₷
            if (numberOfFireBall % 2 == 0)
            {
                numberOfFireBall++;
            }

            // �U���񐔂����߂�ꂽ�l�ɓ��B����܂ōU���𑱂���
            for (int i = 0; i < _FG_numberOfAttack; i++)
            {
                // �A���U���Ԃ̃u���C�N
                yield return _FG_attackRequiredWait;

                // �΂̋ʂ��X�|�[��������
                SpawnEAttackFanForm(PoolEnum.PoolObjectType.fireBullet, _FG_spawnPlace, numberOfFireBall);
            }

            // �U���Ԃ̃u���C�N
            yield return _FG_attackIntervalWait;
        }

        // ��������
        X_Debug.Log("���j�I");
    }

    /// <summary>
    /// FireGatling�ŏ��������G���̃f�N�������g����
    /// </summary>
    private void DecrementFG_NumberOfBird()
    {
        // �r������
        lock (this.gameObject)
        {
            // �ϐ��̃f�N�������g
            _FG_numberOfBirds--;
        }
    }
}