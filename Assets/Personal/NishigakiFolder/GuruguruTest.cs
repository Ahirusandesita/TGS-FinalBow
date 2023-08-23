// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
using UnityEngine;

public class GuruguruTest : MonoBehaviour
{
    #region�@bool�ꗗ
    public bool StartFlag = false;                  //�ړ����J�n����t���O�@true�Ȃ�ړ��@false�Ȃ��~�@�g���Ƃ���true�ɂ��Ă�
    private bool unionFlag = false;                 //�������a�������O�����a�ɍ����������ǂ����𔻒肷��t���O�@����������true�ɂ���
    #endregion

    #region�@Unity�ϐ��ꗗ
    private Vector3 nowPosition = default;          //���ݍ��W�̂R���@localtransfarm�𒼐ڐݒ肷�邽�߂Ɏg�p
    private Vector3 startPos = default;             //�ړ��J�n�n�_�@�g�����ǂ����͕s��
    private Transform goalPos = default;            //�ړ��ڕW�̍��W�@�ŏI�I�ɓ�������ꏊ
    #endregion

    #region�@float�ϐ��ꗗ�@�O������

    private float attract_Power = 1f;              //�����p�ϐ��@�����񂹂�͂̑傫���@�����o������ATTRACT_SPEED�ɕύX

    private float distance = default;               //�����@�ΏۂƖڕW�̋����@�e���̍��̐�Βl�̍��v
                                                    //���񔼌a�����߂邽�߂Ɏg�p
                                                    //���ӎ����Ƃ��Ă�"���a"�ł͂Ȃ�"�Ώە��Ƃ̋���"
    private float startAngle = default;             //�J�n���̒��S���Ƃ̊p�x�@�O�p�֐��̏����l�����߂�̂Ɏg�p
                                                    // �O �� startAngle �� �R�U�O�@�}�C�i�X�ɂȂ�Ȃ��悤�ɕ␳����K�v����
    private float startValue = default;             //�����ln�@�O�p�֐��̏����ʒu��ݒ肷��̂Ɏg�p
                                                    //���F startAngle �� 360 �~ 2��
                                                    //�O�p�֐��̌o�ߗʂ��O�`�P�ŕ\�����l
    private float radius = default;                 //���ar ���_�Ƃ̋����@����͎G�ɎO������
                                                    //����O�����a�֍������邽�߂Ɏg�p
                                                    //�l��local�Ŏ��
    private float addRadius = default;              //���a���Z�l�@�������a�������O����ɏ悹�邽�߂̉��Z�l
                                                    //���̔��a�ɉ��Z���邱�ƂŁA���X�Ɏ���O���֍L���Ă���
    private float difRadius = default;              //���a�̂���@�������a�Ǝ���O�����a�̍��@addRadius�̏���l
                                                    //Lerp�̖ڕW�l�Ƃ��Đݒ肷��
    private float time_AddRadius = default;         //���a�C���̌o�ߎ��ԁ@lerp�Ŏg�p  �o�ߎ��ԁ�LERP_TIME_ADDRADIUS
    private float trajectory = default;             //����O�����a�@�����~�����W���@�������Ƃ̑z��O���~��
    private float moveValue_x = default;            //X���̈ړ��l
    private float moveValue_y = default;            //Y���̈ړ��l
    private float lerp_Time_AddRadius = default;    //�������a�������O���ɏ悹��܂ł̎���  �ڕW�ɓ��B���鎞�Ԃ̔���
    #endregion

    #region�@float�萔�ꗗ
    const float ZERO = 0f;                          //�O�@�[���@�����̃[���@�j���[�X�ԑg�ł͂Ȃ�
    const float ALL_ROUND = 360f;                   //�R�U�O���@���]�@�ł₟�����������I
    const float CUT = 0.7f;                         //�O�D�V�{�@���ꂾ��
    const float COEF_DIST_RADI = 0.1f;              //�����W���@�����ɂ���Ĕ��a�����߂邽�߂̌W��
    const float PERIOD_VALUE = Mathf.PI * 1f;       //��]�����@�΁~�b���Ŏw��@�Ȃ��덷����@�S�~���
    const float straightRange = 0f;                 //��]�����ɂ܂��������ł��鋗���@�O�ł����Ɖ��@
                                                    //N��localtransfarm��N�̒n�_�܂ŉ��N����͂܂��������ł���
    const float ATTRACT_SPEED = default;            //�����񂹂鑬�x�@�ŏ�����ō����������ň����񂹂�@
                                                    //���͒����p��attract_Power�ɂ��Ă��邯�ǁA�ŏI�I�ɂ͂������ɂ��Ăق���
    #endregion

    /// <summary>
    /// ����^���̏����ݒ�@�ŏ��Ɉ�񂾂��Ă�
    /// </summary>
    private void MoveSetUp()
    {
        //�����ʒu�̑���ƌv�Z
        goalPos = transform.parent;
        startPos = transform.localPosition;
        startAngle = Mathf.Atan2(startPos.x, startPos.y) * Mathf.Rad2Deg;

        //��������
        distance = startPos.z;

        //�������a�������O����ɏ悹�邽�߂̎��Ԃ�ݒ�
        lerp_Time_AddRadius = distance / attract_Power * CUT;

        //�����p�x���@�O�`�R�U�O�ɕ␳
        if (startAngle < 0)
        {
            startAngle += 360;
        }

        //�����ln�̌v�Z
        startValue = startAngle / ALL_ROUND * Mathf.PI * 2f;

        //���a�v�Z
        radius = Mathf.Sqrt(Mathf.Pow(transform.localPosition.x, 2f) + Mathf.Pow(transform.localPosition.y, 2f));

    }

    /// <summary>
    /// ����^�����\�b�h�@������A�b�v�f�[�g�ŉ񂹂΂ł���͂�
    /// </summary>
    private void ItemAttract()
    {
        if (!StartFlag)
        {
            MoveSetUp();
        }
        //�t���O��true�ɂȂ�����X�^�[�g
        if (StartFlag)
        {
            //����O���~���v�Z
            trajectory = Mathf.Clamp((distance - straightRange) * COEF_DIST_RADI, ZERO, Mathf.Infinity);

            //�e���ړ��l�v�Z
            //����O����ɏ������̓���
            if (unionFlag)
            {
                //�O���������^���@�e���𒼂Ɏw��
                moveValue_x = Mathf.Sin(startValue + (Time.time) * PERIOD_VALUE) * trajectory;
                moveValue_y = Mathf.Cos(startValue + (Time.time) * PERIOD_VALUE) * trajectory;
            }
            //����O���ֈړ�����]����Ƃ��̓���
            else
            {
                //�������a�Ǝ���O���̍����Z�o
                difRadius = trajectory - radius;

                //�������a����̉��Z�l���Z�o
                addRadius = Mathf.Lerp(ZERO, difRadius, time_AddRadius);
                //Lerp�Ɏg�����Ԍv�Z
                time_AddRadius += (Time.deltaTime) / lerp_Time_AddRadius;

                //�e���̈ʒu���w��
                moveValue_x = Mathf.Sin(startValue + (Time.time) * PERIOD_VALUE) * (radius + addRadius);
                moveValue_y = Mathf.Cos(startValue + (Time.time) * PERIOD_VALUE) * (radius + addRadius);

                //����O���ɏ������I���@���ԂŔ���
                if (time_AddRadius > 1)
                {
                    unionFlag = true;
                }
            }

            //���W�̑������
            nowPosition.x = moveValue_x;
            nowPosition.y = moveValue_y;
            nowPosition.z = distance;

            //�ʒu�̊m��
            transform.localPosition = nowPosition;

            //�������k�߂鏈��
            //�������O����Ȃ��Ȃ������
            if (distance > 0)
            {
                distance -= attract_Power * Time.deltaTime;
            }
            //�������O�ɂȂ�����ړ��I������уI�u�W�F�N�g�̏���
            else
            {
                StartFlag = false;
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�������񂹂鑬�x��ݒ肷�郁�\�b�h
    /// </summary>
    /// <param name="set"></param>
    public void SetAttractPower(float set)
    {
        attract_Power = set;
    }

}
