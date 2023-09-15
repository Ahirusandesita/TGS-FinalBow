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

    // ����t���O�@�����������true�ɂ���
    private bool _endSetting = false;

    // ��̏����@�O���ɐi��ł����͂̑傫��
    private float _arrowSpeed = 10f;

    // Vector.forward�@�����y���̂��߂ɂ��炩���ߑ�����Ă���
    private Vector3 _forward = Vector3.forward;


    /***  �������牺�@�萔  ***/

    // �[���@�����̃[���@���ʂɃ[���@�Ȃ�float �j���[�X�ԑg�ł͂Ȃ�
    private const float ZERO = 0f;

    // �ђʃt���O�@�ђʂ̎��� true
    private const bool PENETRATE = true;

    // �ђʃt���O�@�ђʈȊO�̎��� false
    private const bool NOT_PENETRATE = false;

    // �����@����Ȃ��̃N�����v���Ɏg�p
    private const float INFINITY = Mathf.Infinity;
    #endregion

    #region �m�[�}���Ŏg�p���Ă���ϐ�

    // ������Ă�������x�N�g��
    private Vector3 _arrowVector = default;

    // ��̈ړ��ʁ@�e���������Ƃ̈ړ��ʂ�������
    private Vector3 _moveValue = default;

    // ��̂w�������ւ̈ړ����x
    private float _arrowSpeed_X = default;

    // ��̂x�������ւ̈ړ����x
    private float _arrowSpeed_Y = default;

    // ��̂y�������ւ̈ړ����x
    private float _arrowSpeed_Z = default;

    // ���݂̐��������ւ̈ړ����x�̊����@�ǂ̂��炢���x�������Ă��邩
    private float _nowSpeedValue = default;

    // ��̎�ނɂ��d�͗�
    private float _gravity = default;

    // ��̗������x�̉��Z�l�@_arrowSpeed_Y �ɉ��Z����
    private float _addGravity = default;

    // ��̗������x�̉��Z�l�̏��
    private float _maxGravity = default;

    // ��̎�ނɂ�鑬�x������
    private float _attenuation = default;

    // ��̔�s����
    private float _flightTime = default;



    /***  �������牺�@�萔  ***/

    // �ʏ펞�̏d�͉����x�@��Βl���傫���قǍ~������̂������Ȃ�
    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY_NORMAL = -100f;

    // �T���_�[�̎��̏d�͉����x�@��Βl���傫���قǍ~������̂������Ȃ�
    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY_THUNDER = -10f;

    // �ʏ펞�̎˒��{���@�˒��������قǑ��x���������Ȃ�
    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float NORMAL_ATTENUATION = 0.14286f;

    // �T���_�[�̎��̎˒��{���@�˒��������قǑ��x���������Ȃ�
    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float PENETRATE_ATTENUATION = 0.01f;

    // ���x�����̌��l�@���ݑ��x = STANDARD_SPEED_VALUE - ������
    private const float STANDARD_SPEED_VALUE = 1f;

    // ���x�����̍ő�l
    private const float MAXIMUM_SPEED_VALUE = 1f;

    // �ő嗎�����x�@�������x���������钸�_�@�����葬���͗������Ȃ�
    [Tooltip("�ő嗎�����x�@�������x���������钸�_�@�����葬���͗������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float TERMINAL_VELOCITY = -1500f;

    // ���[�v��̖�̏������x
    [SerializeField, Tooltip("���[�v��̖�̏������x")]
    private float AFTER_WARP_SPEED = 100f;

    #endregion

    #region �z�[�~���O�Ŏg�p���Ă���ϐ�

    // �z�[�~���O����^�[�Q�b�g
    private Transform _target = default;

    // �^�[�Q�b�g�֌������߂̃x�N�g��
    private Vector3 _lookVect = default;

    // �ŏI�I�Ɍ�������
    private Quaternion _lookRot = default;

    // �^�[�Q�b�g�̕��������鑬�x
    private float _lookSpeed = default;

    // ���Ԍo�߂ɂ���đ�����������̒������x�̌W��
    private float _lookSpeedCoefficient = LOOKSPEED_DEFAULT;

    // LockOn�Ɏg���N���X
    private LockOnSystem _lockOnSystem = default;



    /***  �������牺�@�萔  ***/

    // �����̑��x�W���@�F�X�����Ă݂Ă�
    private const float SPEED_COEF = 0.00074f;

    // �^�[�Q�b�g��T���~���̒��S�p�@���݂͂R�U�O�x���ׂăT�[�`����@�l�͒��S�p�̔���������
    private const int SEARCH_ANGLE = 1;

    // _lookSpeedCoefficient�̏����l
    private const float LOOKSPEED_DEFAULT = 1f;

    // _lookSpeedCoefficient�̑����W��
    private const float LOOKSPEED_ADDVALUE = 2f;

    // _lookSpeedCoefficient�̍ő�l
    private const float LOOKSPEED_MAX = 4f;

    #endregion

    #endregion

    #region �v���p�e�B

    /// <summary>
    /// ��C��R�̐ݒ�v���p�e�B�@�ʏ킩�ђʂŕω�
    /// </summary>
    /// <param name="isPenetrate">�ђʃt���O</param>
    /// <returns></returns>
    private float Attenuation(bool isPenetrate)
    {
        // �ђʂ��ǂ�������
        if (isPenetrate)
        {
            // �ђʂ̋�C��R��Ԃ�
            return PENETRATE_ATTENUATION;
        }
        else
        {
            // �ʏ�̋�C��R��Ԃ�
            return NORMAL_ATTENUATION;
        }
    }

    /// <summary>
    /// �d�͉����x�̐ݒ�v���p�e�B�@�ʏ킩�ђʂŕω�
    /// </summary>
    /// <param name="isPenetrate">�ђʃt���O</param>
    /// <returns></returns>
    private float GravityValue(bool isPenetrate)
    {
        // �ђʂ��ǂ�������
        if (isPenetrate)
        {
            // �ђʂ̏d�͉����x��Ԃ�
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
    // �C�x���g�̐ݒ���s���Ă��邩�ǂ�������
    private bool _isSet = false;

    // ��̋������Ǘ�����f���Q�[�g
    private delegate void MovementDelegate(Transform arrowTransform, bool isThunder);

    // ��̋�����o�^����C�x���g
    MovementDelegate movement;

    /// <summary>
    /// �ʏ�̖�̋����C�x���g��o�^���郁�\�b�h
    /// </summary>
    private void SetNormal()
    {
        movement = NormalMove;
        _isSet = true;
    }

    /// <summary>
    /// �z�[�~���O�̖�̋����C�x���g��o�^���郁�\�b�h
    /// </summary>
    private void SetHoming()
    {
        movement = HomingMove;
        _isSet = true;
    }

    #endregion

    #region �m�[�}���̋���
    /// <summary>
    /// �m�[�}���̋����������郁�\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="isPenetrate">�ђʂ�����ȊO��</param>
    private void NormalMove(Transform arrowTransform, bool isPenetrate)
    {
        // �ݒ肪�I����Ă��Ȃ����
        if (!_endSetting)
        {
            // �ݒ�p���\�b�h�����s
            NormalSetting(arrowTransform, _arrowSpeed, isPenetrate);
        }

        // �ݒ肪�I����Ă�����
        else
        {
            // ���������ւ̈ړ����x�̌��������Z�o
            _nowSpeedValue = Mathf.Clamp(STANDARD_SPEED_VALUE - (_flightTime * _attenuation), ZERO , MAXIMUM_SPEED_VALUE);

            // �e�������ւ̈ړ��ʂ��Z�o
            _moveValue.x = (_arrowSpeed_X * _nowSpeedValue);    // �w��
            _moveValue.y = (_arrowSpeed_Y + _addGravity);       // �x��
            _moveValue.z = (_arrowSpeed_Z * _nowSpeedValue);    // �y��

            // �e�������ֈړ��ʂ̕������ړ�
            arrowTransform.position += _moveValue * Time.deltaTime;

            // �ړ����Ă�������Ɍ�������
            arrowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

            // ���݂̈ړ����������Z
            _flightTime += Time.deltaTime;

            // �d�͂ɂ�鉺�����ւ̈ړ��ʂ��Z�o
            _addGravity = Mathf.Clamp(_addGravity + _gravity * Time.deltaTime, _maxGravity , INFINITY);
        }
    }

    /// <summary>
    /// �m�[�}���̋����̂��߂̏����ݒ���s�����\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł����X�s�[�h</param>
    /// <param name="isPenetrate">�ђʂ�����ȊO��</param>
    private void NormalSetting(Transform arrowTransform, float arrowSpeed, bool isPenetrate)
    {
        // ��̊p�x���擾
        _arrowVector = arrowTransform.TransformVector(_forward).normalized;

        // ��̊e�������ւ̈ړ����x���Z�o
        _arrowSpeed_X = _arrowVector.x * arrowSpeed;    // �w��
        _arrowSpeed_Y = _arrowVector.y * arrowSpeed;    // �x��
        _arrowSpeed_Z = _arrowVector.z * arrowSpeed;    // �y��

        // ��̑��x�����ʂ�ݒ�
        _attenuation = Attenuation(isPenetrate);

        // ��̏d�͗ʂ�ݒ�
        _gravity = GravityValue(isPenetrate);

        // �x���̑��x����~���ʂ̏���l���Z�o
        _maxGravity = Mathf.Clamp(TERMINAL_VELOCITY - _arrowSpeed_Y, -INFINITY ,ZERO);

        // ��̔�s���Ԃ̏�����
        _flightTime = default;

        // �d�͂ɂ��ړ��ʂ�������
        _addGravity = default;

        // �ݒ芮��
        _endSetting = true;
    }

    /// <summary>
    /// �ʏ�̖�̋����̐ݒ���������E�Đݒ肷�郁�\�b�h
    /// </summary>
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
    /// <param name="isPenetrate">�ђʂ�����ȊO��</param>
    private void HomingMove(Transform arrowTransform, bool isPenetrate)
    {
        // �����ݒ�
        if (!_endSetting)
        {
            HomingSetting(arrowTransform , _arrowSpeed);
        }

        // �␳�����ԂƂƂ��ɑ���
        if(_lookSpeedCoefficient < LOOKSPEED_MAX)
        {
            _lookSpeedCoefficient += LOOKSPEED_ADDVALUE * Time.deltaTime;
        }

        // �^�[�Q�b�g�ւ̃x�N�g�����擾
        _lookVect = _target.position - arrowTransform.position;

        // �ŏI�p�x��ݒ�
        _lookRot = Quaternion.LookRotation(_lookVect);

        // �I�u�W�F�N�g��rotation��ύX
        arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation, _lookRot, _lookSpeed * _lookSpeedCoefficient);

        // ��̈ړ�
        arrowTransform.Translate( ZERO,                               // �w��
                                  ZERO,                               // �x��
                                  _arrowSpeed * Time.deltaTime,        // �y��
                                  Space.Self);                        // ���[�J���Ŏw��@���͂y��

        // �^�[�Q�b�g����ꂽ�狓���ύX
        if (_target.gameObject.activeSelf == false)
        {
            _endSetting = false;
            SetNormal();
        }
    }

    /// <summary>
    /// �z�[�~���O�̏����ݒ�ƃ^�[�Q�b�g�̑I����s�����\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł����X�s�[�h</param>
    private void HomingSetting(Transform arrowTransform, float arrowSpeed)
    {
        // �t���O�̐ݒ�@����������獡�������Ȃ��悤�ɕύX
        _endSetting = true;

        // �ǔ����\�𑬓x�ɂ���č����ł��Ȃ��悤�ɐݒ�
        _lookSpeed = SPEED_COEF * arrowSpeed;

        // ��O�����@����������ɃI�u�W�F�N�g������Ȃ������ꍇ��nullRef��������邽�߂̏���
        if (_target == null || _target == default)
        {
            // �܂��������ōs���悤�ɕύX
            SetNormal();
            _endSetting = false;
        }
    }

    #endregion

    #region�@���p���\�b�h
    /// <summary>
    /// ���b�N�I���p�̃N���X��o�^
    /// </summary>
    private void Start()
    {
        // LockOnSystem�̑��
        _lockOnSystem = GameObject.FindGameObjectWithTag(InhallLibTags.BowController).GetComponent<LockOnSystem>();
    }

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
        movement(t, NOT_PENETRATE);
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
    #endregion

    #endregion
}
