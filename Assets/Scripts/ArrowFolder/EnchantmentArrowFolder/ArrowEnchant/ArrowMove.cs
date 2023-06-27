// --------------------------------------------------------- 
// ArrowMove.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
// --------------------------------------------------------- 
using UnityEngine;
using Nekoslibrary;

interface IArrowMoveSetting
{
    float SetArrowSpeed { set; }
}
interface IArrowMoveReset
{
    void ResetsStart();
}
interface IArrowMoveSettingReset : IArrowMoveSetting, IArrowMoveReset
{

}


/// <summary>
/// ��̓����̃N���X�@�ʏ�ƃz�[�~���O�̋������s��
/// </summary>
public class ArrowMove : MonoBehaviour, IArrowMoveSettingReset
{

    #region �ϐ� �O������


    #region ���p�ϐ�

    // �����̊p�x�@����t���O�������Ă�����������
    private Vector3 _firstAngle = default;

    // ����t���O�@�����������true�ɂ���
    private bool _isSetAngle = false;

    // ��̏����@�O���ɐi��ł����͂̑傫��
    // SetArrowSpeed�ő��x���w��ł���
    private float _arrowSpeed = 10f;


    // �萔

    // �[���@�����̃[���@���ʂɃ[���@�Ȃ�float �j���[�X�ԑg�ł͂Ȃ�
    private const float ZERO = 0f;

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

    // ��̐��������ւ̈ړ����x�̌�����
    private float _nowSpeedValue = default;

    // ��̍~�����鑬�x�@_arrowSpeed_Y �ɉ��Z����
    private float _addGravity = default;

    // �ݒ肪�I��������ǂ������肷��t���O
    private bool _endSetting = false;



    /***  �������牺�@�萔  ***/

    // �d�͉����x�@�傫���قǍ~������̂������Ȃ�
    [Tooltip("�d�͉����x�@�傫���قǍ~������̂������Ȃ�@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float GRAVITY = -50f;

    // ��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ�
    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����"),SerializeField] //�f�o�b�O�p
    private float SPEED_TO_RANGE_COEFFICIENT_NORMAL = 15f;

    // ��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ�
    [Tooltip("��̎˒������߂�l�@�˒��������قǑ��x���������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float SPEED_TO_RANGE_COEFFICIENT_THUNDER = 30f;

    // ���x�����̌��l�@���ݑ��x = STANDARD_SPEED_VALUE - ������
    private const float STANDARD_SPEED_VALUE = 1f;

    // �ő嗎�����x�@�������x���������钸�_�@�����葬���͗������Ȃ�
    [Tooltip("�ő嗎�����x�@�������x���������钸�_�@�����葬���͗������Ȃ��@�������I�������V���A���C�Y����"), SerializeField] //�f�o�b�O�p
    private float TERMINAL_VELOCITY = 100f;

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

    // ����������ɓG�����Ȃ��������̒��i�p�^�[�Q�b�g
    private GameObject _tmpTarget = default;

    // �萔

    // �����̑��x�W���@�F�X�����Ă݂Ă�
    private const float SPEED_COEF = 0.00074f;

    // �^�[�Q�b�g��T���~���̒��S�p�@���݂͂R�U�O�x���ׂăT�[�`����@�l�͒��S�p�̔���������
    private const float SEARCH_ANGLE = 180f;

    // Vector.forward�@�����y���̂��߂ɂ��炩���ߑ�����Ă���
    private Vector3 _forward = Vector3.forward;

    #endregion

    #endregion

    public void ArrowMove_Nomal(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_Bomb(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_Thunder(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, true); }





    public void ArrowMove_KnockBack(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_Homing(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Penetrate(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_BombThunder(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, true); }





    public void ArrowMove_BombKnockBack(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_BombHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombPenetrate(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_ThunderKnockBack(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, true); }





    public void ArrowMove_ThunderHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderPenetrate(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, true); }





    public void ArrowMove_KnockBackHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBackPenetrate(Transform arrowTransform) { NormalMove2(arrowTransform, _arrowSpeed, false); }





    public void ArrowMove_HomingPenetrate(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }



    /// <summary>
    /// �z�[�~���O�̋����������郁�\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł����X�s�[�h</param>
    private void HomingMove(Transform arrowTransform, float arrowSpeed)
    {
        // �����p�x�̑���@�^�[�Q�b�g���Ȃ��Ȃ����ꍇ�͍ēx���
        if (_target == null || _target == default)
        {
            // �����ݒ�ƃ^�[�Q�b�g�̑I��
            SetHomingTarget(arrowTransform, arrowSpeed);
        }


        // �^�[�Q�b�g�ւ̃x�N�g�����擾
        _lookVect = _target.transform.position - arrowTransform.position;
        // �ŏI�p�x��ݒ�
        _lookRot = Quaternion.LookRotation(_lookVect);
        // �I�u�W�F�N�g��rotation��ύX
        arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation, _lookRot, _lookSpeed);

        // ��̈ړ�
        arrowTransform.Translate(ZERO,                               // �w��
                                    ZERO,                               // �x��
                                    arrowSpeed * Time.deltaTime,        // �y��
                                    Space.Self);                        // ���[�J�����W�Ŏw��@����Z���Ɍ����Ă���O��
    }

    /// <summary>
    /// �z�[�~���O�̏����ݒ�ƃ^�[�Q�b�g�̑I����s�����\�b�h
    /// </summary>
    /// <param name="arrowTransform"></param>
    /// <param name="arrowSpeed"></param>
    private void SetHomingTarget(Transform arrowTransform, float arrowSpeed)
    {
        // �������̏����������ƃ^�[�Q�b�g�đI��
        // �����̊p�x�����[���h���W�̐��l�ő���@�F�X�g��
        _firstAngle = new Vector3(arrowTransform.eulerAngles.x, arrowTransform.eulerAngles.y, arrowTransform.eulerAngles.z);

        // �t���O�̐ݒ�@����������獡�������Ȃ��悤�ɕύX
        _isSetAngle = !_isSetAngle;

        // �ǔ����\�𑬓x�ɂ���č����ł��Ȃ��悤�ɐݒ�
        _lookSpeed = SPEED_COEF * arrowSpeed;

        // �^�[�Q�b�g��T�����đ��
        _target = ConeDecision.ConeSearchNearest(arrowTransform, AttractObjectList.GetAttractObject(), SEARCH_ANGLE);


        // ��O�����@����������ɃI�u�W�F�N�g������Ȃ������ꍇ��nullRef��������邽�߂̏���
        if (_target == null || _target == default)
        {
            // �܂��������ōs���悤�Ƀ^�[�Q�b�g����
            _target = _tmpTarget;
        }
    }



    /// <summary>
    /// NormalMove�J�n���̐ݒ胁�\�b�h
    /// </summary>
    private void NormalSetting(Transform arrowTransform, float arrowSpeed, bool isThunder)
    {
        _arrowVector = arrowTransform.TransformVector(_forward).normalized;
        _arrowSpeed_Horizontal = Mathf.Sqrt(MathN.Pow(_arrowVector.x) + MathN.Pow(_arrowVector.z)) * arrowSpeed;
        _arrowSpeed_X = _arrowVector.x * arrowSpeed;
        _arrowSpeed_Y = _arrowVector.y * arrowSpeed;
        _arrowSpeed_Z = _arrowVector.z * arrowSpeed;
        _maxRange = _arrowSpeed_Horizontal * SpeedToRangeCoefficient(isThunder);

        _nowRange = default;
        _addGravity = default;

        _endSetting = true;
    }

    /// <summary>
    /// �ʏ�̖�̋���
    /// </summary>
    /// <param name="arrowTransform"></param>
    /// <param name="arrowSpeed"></param>
    /// <param name="isThunder"></param>
    private void NormalMove2(Transform arrowTransform, float arrowSpeed, bool isThunder)
    {
        if (!_endSetting)
        {
            NormalSetting(arrowTransform, arrowSpeed, isThunder);
        }

        _nowSpeedValue = MathN.Clamp_min( STANDARD_SPEED_VALUE - (_nowRange / _maxRange), ZERO );

        _moveValue.x = (_arrowSpeed_X * _nowSpeedValue);
        _moveValue.y = (_arrowSpeed_Y + _addGravity);
        _moveValue.z = (_arrowSpeed_Z * _nowSpeedValue);

        arrowTransform.position +=  _moveValue * Time.deltaTime ;
        arrowTransform.rotation = Quaternion.LookRotation(_moveValue.normalized, _forward);

        _nowRange += _arrowSpeed_Horizontal * Time.deltaTime;
        _addGravity = MathN.Clamp_max( _addGravity + GRAVITY * Time.deltaTime, TERMINAL_VELOCITY + _arrowSpeed_Y);
    }

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
    /// ��̑��x���w�肷��v���p�e�B�@set�����Ȃ����ߒ���
    /// </summary>
    public float SetArrowSpeed
    {
        set
        {
            _arrowSpeed = value / 10;
        }
    }

    /// <summary>
    /// ���X�^�[�g�ݒ�p�v���p�e�B
    /// </summary>
    public void ResetsStart()
    {
        _isSetAngle = false;
        _endSetting = false;
    }

}
