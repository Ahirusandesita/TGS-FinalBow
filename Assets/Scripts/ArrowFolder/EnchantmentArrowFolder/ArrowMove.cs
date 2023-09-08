// --------------------------------------------------------- 
// ArrowMove.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;



#region �C���^�[�t�F�[�X

/// <summary>
/// ��̋����̐ݒ�̂��߂ɕK�v�ȃC���^�[�t�F�[�X
/// </summary>
interface IArrowMoveSetting
{
    /// <summary>
    /// ��̑��x���w�肷��v���p�e�B�@set�����Ȃ����ߒ���
    /// </summary>
    float SetArrowSpeed { set; }
}
/// <summary>
/// ��̋����̃��Z�b�g�̂��߂ɕK�v�ȃC���^�[�t�F�[�X
/// </summary>
interface IArrowMoveReset
{
    /// <summary>
    /// ��̋����̃��Z�b�g�������\�b�h
    /// </summary>
    void ResetsStart();
}

/// <summary>
/// ArrowMove�ɕK�v�ȃC���^�[�t�F�[�X
/// </summary>
interface IArrowMoveSettingReset : IArrowMoveSetting, IArrowMoveReset
{

}

#endregion


/// <summary>
/// ��̓����̃N���X�@�ʏ�ƃz�[�~���O�̋������s��
/// </summary>
public class ArrowMove : MonoBehaviour, IArrowMoveSettingReset,IArrowEnchantable<Transform>
{
    #region �ϐ��ꗗ �O������

    #region ���p�ϐ�

    // �����̊p�x�@����t���O�������Ă�����������
    private Vector3 _firstAngle = default;

    // ����t���O�@�����������true�ɂ���
    private bool _endSetting = false;

    // ��̏����@�O���ɐi��ł����͂̑傫��
    private float _arrowSpeed = 10f;

    // Vector.forward�@�����y���̂��߂ɂ��炩���ߑ�����Ă���
    private Vector3 _forward = Vector3.forward;


    /***  �������牺�@�萔  ***/

    // �[���@�����̃[���@���ʂɃ[���@�Ȃ�float �j���[�X�ԑg�ł͂Ȃ�
    private const float ZERO = 0f;

    // �T���_�[�t���O�@�T���_�[�̎��� true
    private const bool PENETRATE = true;

    // �T���_�[�t���O�@�T���_�[�ȊO�̎��� false
    private const bool NOT_PENETRATE = false;

    // �����@����Ȃ��̃N�����v���Ɏg�p
    private const float INFINITY = Mathf.Infinity;
    #endregion

    #region �m�[�}���Ŏg�p���Ă���ϐ�

    // ������Ă�������x�N�g��
    private Vector3 _arrowVector = default;

    // ��̈ړ��ʁ@�e���������Ƃ̈ړ��ʂ�������
    private Vector3 _moveValue = default;

    // ��̐��������ւ̈ړ����x
    private float _arrowSpeed_Horizontal = default;

    // ��̂w�������ւ̈ړ����x
    private float _arrowSpeed_X = default;

    // ��̂x�������ւ̈ړ����x
    private float _arrowSpeed_Y = default;

    // ��̂y�������ւ̈ړ����x
    private float _arrowSpeed_Z = default;

    // �ő�̈ړ������@���x�ɉ����ĕω�����
    private float _maxRange = default;

    // ���݂̈ړ�����
    private float _nowRange = default;

    // ���݂̐��������ւ̈ړ����x�̊����@�ǂ̂��炢���x�������Ă��邩
    private float _nowSpeedValue = default;

    // ��̗������x�̉��Z�l�@_arrowSpeed_Y �ɉ��Z����
    private float _addGravity = default;

    // ��̗������x�̉��Z�l�̏��
    private float _maxGravity = default;



    /***  �������牺�@�萔  ***/

    // �ʏ펞�̏d�͉����x�@��Βl���傫���قǍ~������̂������Ȃ�
    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY_NORMAL = -100f;

    // �T���_�[�̎��̏d�͉����x�@��Βl���傫���قǍ~������̂������Ȃ�
    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY_THUNDER = -10f;

    // �ʏ펞�̎˒��{���@�˒��������قǑ��x���������Ȃ�
    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float SPEED_TO_RANGE_COEFFICIENT_NORMAL = 7f;

    // �T���_�[�̎��̎˒��{���@�˒��������قǑ��x���������Ȃ�
    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float SPEED_TO_RANGE_COEFFICIENT_THUNDER = 100f;

    // ���x�����̌��l�@���ݑ��x = STANDARD_SPEED_VALUE - ������
    private const float STANDARD_SPEED_VALUE = 1f;

    // �ő嗎�����x�@�������x���������钸�_�@�����葬���͗������Ȃ�
    [Tooltip("�ő嗎�����x�@�������x���������钸�_�@�����葬���͗������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float TERMINAL_VELOCITY = -1500f;

    // ���[�v��̖�̏������x
    [SerializeField, Tooltip("���[�v��̖�̏������x")]
    private float AFTER_WARP_SPEED = 100f;

    #endregion

    #region �z�[�~���O�Ŏg�p���Ă���ϐ�

    // �z�[�~���O����^�[�Q�b�g
    private GameObject _target = default;

    // �^�[�Q�b�g�֌������߂̃x�N�g��
    private Vector3 _lookVect = default;

    // �ŏI�I�Ɍ�������
    private Quaternion _lookRot = default;

    // �^�[�Q�b�g�̕��������鑬�x
    private float _lookSpeed = default;

    // ���Ԍo�߂ɂ���đ�����������̒������x�̌W��
    private float _lookSpeedCoefficient = LOOKSPEED_DEFAULT;
    
    // ����������ɓG�����Ȃ��������̒��i�p�^�[�Q�b�g
    private GameObject _tmpTarget = default;

    // ������Ȃ������ꍇtrue
    private bool _cantGet = false;

    // LockOn�Ɏg���N���X
    private LockOnSystem _lockOnSystem = default;



    /***  �������牺�@�萔  ***/

    // �����̑��x�W���@�F�X�����Ă݂Ă�
    private const float SPEED_COEF = 0.00074f;

    // �^�[�Q�b�g��T���~���̒��S�p�@���݂͂R�U�O�x���ׂăT�[�`����@�l�͒��S�p�̔���������
    private const int SEARCH_ANGLE = 1;

    // _lookSpeedCoefficient�̏����l
    private const float LOOKSPEED_DEFAULT = 0.5f;

    // _lookSpeedCoefficient�̑����W��
    private const float LOOKSPEED_ADDVALUE = 1f;

    // _lookSpeedCoefficient�̍ő�l
    private const float LOOKSPEED_MAX = 3f;

    #endregion

    #endregion

    #region �v���p�e�B

    /// <summary>
    /// ��C��R�̐ݒ�v���p�e�B�@�ʏ킩�T���_�[�ŕω�
    /// </summary>
    /// <param name="isThunder">�T���_�[�t���O</param>
    /// <returns></returns>
    private float SpeedToRangeCoefficient(bool isThunder)
    {
        // �T���^�[���ǂ�������
        if (isThunder)
        {
            // �T���_�[�̋�C��R��Ԃ�
            return SPEED_TO_RANGE_COEFFICIENT_THUNDER;
        }
        else
        {
            // �ʏ�̋�C��R��Ԃ�
            return SPEED_TO_RANGE_COEFFICIENT_NORMAL;
        }
    }

    /// <summary>
    /// �d�͉����x�̐ݒ�v���p�e�B�@�ʏ킩�T���_�[�ŕω�
    /// </summary>
    /// <param name="isThunder">�T���_�[�t���O</param>
    /// <returns></returns>
    private float GravityValue(bool isThunder)
    {
        // �T���_�[���ǂ�������
        if (isThunder)
        {
            // �T���_�[�̏d�͉����x��Ԃ�
            return GRAVITY_THUNDER;
        }
        else
        {
            // �ʏ�̏d�͉����x��Ԃ�
            return GRAVITY_NORMAL;
        }
    }

    /// <summary>
    /// ��̑��x���w�肷��v���p�e�B�@set�����Ȃ����ߒ���
    /// </summary>
    public float SetArrowSpeed
    {
        set
        {
            // ��̑��x��ݒ�
            _arrowSpeed = value;
        }
    }

    #endregion

    #region ���\�b�h

    #region �C�x���g�ݒ�p���\�b�h

    ///// <summary>
    ///// ��̋����@�G���`�����g�F�Ȃ�
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_Normal(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�{��
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_Bomb(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�T���_�[
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_Thunder(Transform arrowTransform) { NormalMove(arrowTransform, THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�m�b�N�o�b�N
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_KnockBack(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�z�[�~���O
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_Homing(Transform arrowTransform) { HomingMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�ђ�
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_Penetrate(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�{���T���_�[
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_BombThunder(Transform arrowTransform) { NormalMove(arrowTransform, THUNDER); }


    ///// <summary>
    ///// ��̋����@�G���`�����g�F�{���m�b�N�o�b�N
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_BombKnockBack(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�{���z�[�~���O
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_BombHoming(Transform arrowTransform) { HomingMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�{���ђ�
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_BombPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�T���_�[�m�b�N�o�b�N
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_ThunderKnockBack(Transform arrowTransform) { NormalMove(arrowTransform, THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�T���_�[�z�[�~���O
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_ThunderHoming(Transform arrowTransform) { HomingMove(arrowTransform, THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�T���_�[�ђ�
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_ThunderPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�m�b�N�o�b�N�z�[�~���O
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_KnockBackHoming(Transform arrowTransform) { HomingMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�m�b�N�o�b�N�ђ�
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_KnockBackPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, NOT_THUNDER); }



    ///// <summary>
    ///// ��̋����@�G���`�����g�F�z�[�~���O�ђ�
    ///// </summary>
    ///// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    //public void ArrowMove_HomingPenetrate(Transform arrowTransform) { HomingMove(arrowTransform, NOT_THUNDER); }


    private bool _isSet = false;

    private delegate void MovementDelegate(Transform arrowTransform, bool isThunder);

    MovementDelegate movement;

    private void SetNormal()
    {
        movement = NormalMove;
    }

    private void SetHoming()
    {
        movement = HomingMove;
    }

    #endregion

    #region �m�[�}���̋���
    float t = default;
    float m = default;
    /// <summary>
    /// �m�[�}���̋����������郁�\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="_arrowSpeed">����ł����X�s�[�h</param>
    /// <param name="isThunder">�T���_�[������ȊO��</param>
    private void NormalMove(Transform arrowTransform, bool isThunder)
    {
        // �ݒ肪�I����Ă��Ȃ����
        if (!_endSetting)
        {
            // �ݒ�p���\�b�h�����s
            NormalSetting(arrowTransform, _arrowSpeed, isThunder);
        }

        // �ݒ肪�I����Ă�����
        else
        {
            // ���������ւ̈ړ����x�̌��������Z�o
            _nowSpeedValue = Mathf.Clamp(STANDARD_SPEED_VALUE - (_nowRange / _maxRange), ZERO , Mathf.Infinity);

            // �e�������ւ̈ړ��ʂ��Z�o
            _moveValue.x = (_arrowSpeed_X * _nowSpeedValue);    // �w��
            _moveValue.y = (_arrowSpeed_Y + _addGravity);       // �x��
            _moveValue.z = (_arrowSpeed_Z * _nowSpeedValue);    // �y��

            /*-------------------------------�f�o�b�O�p-------------------------------*/
            //t += Time.deltaTime;
            //if(t> 0.1f)
            //{
            //    print("������:" + _nowSpeedValue + "�@�@������:" + (m - _nowSpeedValue) + "�@�@���ݑ��x:" + _arrowSpeed_Horizontal * _nowSpeedValue);
            //    t = 0f;
            //    m = _nowSpeedValue;
            //}

            // �e�������ֈړ��ʂ̕������ړ�
            arrowTransform.position += _moveValue * Time.deltaTime;

            // �ړ����Ă�������Ɍ�������
            arrowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

            // ���݂̈ړ����������Z
            _nowRange += _arrowSpeed_Horizontal * Time.deltaTime;

            // �d�͂ɂ�鉺�����ւ̈ړ��ʂ��Z�o
            _addGravity = Mathf.Clamp(_addGravity + GravityValue(isThunder) * Time.deltaTime, _maxGravity , INFINITY);


            /*
             �@�ǁX�������鍀��

                �x�������ɑ΂����C��R
                �I�[���x�Ƌ�C��R�A�d�͉����x�̑��݊֌W���͂����肳����
                �����I�ɂǂ��������Ă��s�����N����Ȃ��悤�ɂ���
             */
        }
    }

    /// <summary>
    /// �m�[�}���̋����̂��߂̏����ݒ���s�����\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł����X�s�[�h</param>
    /// <param name="isThunder">�T���_�[������ȊO��</param>
    private void NormalSetting(Transform arrowTransform, float arrowSpeed, bool isThunder)
    {
        // ��̊p�x���擾
        _arrowVector = arrowTransform.TransformVector(_forward).normalized;

        // ��̐��������ւ̈ړ����x���Z�o
        _arrowSpeed_Horizontal = Mathf.Sqrt(Mathf.Pow(_arrowVector.x,2f) + Mathf.Pow(_arrowVector.z ,2f)) * arrowSpeed;

        // ��̊e�������ւ̈ړ����x���Z�o
        _arrowSpeed_X = _arrowVector.x * arrowSpeed;    // �w��
        _arrowSpeed_Y = _arrowVector.y * arrowSpeed;    // �x��
        _arrowSpeed_Z = _arrowVector.z * arrowSpeed;    // �y��

        // ���x����ő�̈ړ��������Z�o
        _maxRange = _arrowSpeed_Horizontal * SpeedToRangeCoefficient(isThunder);

        // �x���̑��x����~���ʂ̏���l���Z�o
        _maxGravity = Mathf.Clamp(TERMINAL_VELOCITY - _arrowSpeed_Y, -INFINITY ,ZERO);

        // ���݂̈ړ����x��������
        _nowRange = default;

        // �d�͂ɂ��ړ��ʂ�������
        _addGravity = default;

        // �ݒ芮��
        _endSetting = true;
    }

    public void ReSetNormalSetting()
    {
        NormalSetting(this.transform, AFTER_WARP_SPEED, false);
    }
    #endregion

    #region �z�[�~���O�̋���

    /// <summary>
    /// �z�[�~���O�̋����������郁�\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="_arrowSpeed">����ł����X�s�[�h</param>
    private void HomingMove(Transform arrowTransform, bool isThunder)
    {
        //// �����p�x�̑���@�^�[�Q�b�g���Ȃ��Ȃ����ꍇ�͍ēx���
        //if (_target == null || _target == default)
        //{
        //    // �����ݒ�ƃ^�[�Q�b�g�̑I��
        //    SetHomingTarget(arrowTransform, _arrowSpeed);
        //    if (_cantGet)
        //    {
        //        _lookSpeedCoefficient = 0f;
        //        _cantGet = false;
        //        _isSet = true;
        //        return;
        //    }
        //}

        if (!_endSetting)
        {
            SetHomingTarget(arrowTransform , _arrowSpeed);
        }

        if(_lookSpeedCoefficient < LOOKSPEED_MAX)
        {
            _lookSpeedCoefficient += LOOKSPEED_ADDVALUE * Time.deltaTime;
        }

        // �^�[�Q�b�g�ւ̃x�N�g�����擾
        _lookVect = _target.transform.position - arrowTransform.position;

        // �ŏI�p�x��ݒ�
        _lookRot = Quaternion.LookRotation(_lookVect);

        // �I�u�W�F�N�g��rotation��ύX
        arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation, _lookRot, _lookSpeed * _lookSpeedCoefficient);

        // ��̈ړ�
        arrowTransform.Translate( ZERO,                               // �w��
                                  ZERO,                               // �x��
                                  _arrowSpeed * Time.deltaTime,        // �y��
                                  Space.Self);                        // ���[�J���Ŏw��@���͂y��
    }

    /// <summary>
    /// �z�[�~���O�̏����ݒ�ƃ^�[�Q�b�g�̑I����s�����\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł����X�s�[�h</param>
    private void SetHomingTarget(Transform arrowTransform, float arrowSpeed)
    {
        // �������̏����������ƃ^�[�Q�b�g�đI��
        // �����̊p�x�����[���h���W�̐��l�ő���@�F�X�g��
        _firstAngle = new Vector3(arrowTransform.eulerAngles.x, arrowTransform.eulerAngles.y, arrowTransform.eulerAngles.z);

        // �t���O�̐ݒ�@����������獡�������Ȃ��悤�ɕύX
        _endSetting = true;

        // �ǔ����\�𑬓x�ɂ���č����ł��Ȃ��悤�ɐݒ�
        _lookSpeed = SPEED_COEF * arrowSpeed;

        // ��O�����@����������ɃI�u�W�F�N�g������Ȃ������ꍇ��nullRef��������邽�߂̏���
        if (_target == null || _target == default)
        {
            // �܂��������ōs���悤�ɕύX
            SetNormal();
            _cantGet = true ;
            _endSetting = false;
            print("setNormal");
        }
    }

    #endregion

    #region�@���p���\�b�h

    /// <summary>
    /// ��̋����̃��Z�b�g�������\�b�h
    /// </summary>
    public void ResetsStart()
    {
        // ��̏����ݒ芮���̃t���O�����Z�b�g
        _endSetting = false;
        _isSet = false;

        // �ꕔ�ϐ��̏�����
        _lookSpeedCoefficient = LOOKSPEED_DEFAULT;
    }

    public void Normal(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Bomb(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Thunder(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void KnockBack(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void Penetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void Homing(Transform t)
    {
        if (!_isSet)
        {
            if(_lockOnSystem.LockOnTarget == null)
            {
                SetNormal();
            }
            else
            {
                _target = _lockOnSystem.TargetSet(this);
                SetHoming();
            }

        }
        movement(t, NOT_PENETRATE);
    }

    public void BombThunder(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void BombKnockBack(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void BombPenetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void BombHoming(Transform t)
    {
        if (!_isSet)
        {
            SetHoming();
        }
        movement(t, NOT_PENETRATE);
    }

    public void ThunderKnockBack(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void ThunderPenetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void ThunderHoming(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, PENETRATE);
    }

    public void KnockBackPenetrate(Transform t)
    {
        if (!_isSet)
        {
            SetNormal();
        }
        movement(t, NOT_PENETRATE);
    }

    public void KnockBackHoming(Transform t)
    {
        if (!_isSet)
        {
            SetHoming();
        }
        movement(t, NOT_PENETRATE);
    }

    public void PenetrateHoming(Transform t)
    {
        if (!_isSet)
        {
            SetHoming();
        }
        movement(t, NOT_PENETRATE);
    }

    private void Start()
    {
        _lockOnSystem = GameObject.FindObjectOfType<BowManager>().GetComponent<LockOnSystem>();
    }

    #endregion

    #endregion
}
