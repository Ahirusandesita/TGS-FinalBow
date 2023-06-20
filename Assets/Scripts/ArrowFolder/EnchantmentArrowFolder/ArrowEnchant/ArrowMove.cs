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
interface IArrowMoveSettingReset:IArrowMoveSetting,IArrowMoveReset
{

}


/// <summary>
/// ��̓����̃N���X�@�ʏ�ƃz�[�~���O�̋������s��
/// </summary>
public class ArrowMove : MonoBehaviour,IArrowMoveSettingReset
{

    #region �ϐ� �O������

    #region ���p�ϐ�

    // �����̊p�x�@����t���O�������Ă�����������
    private Vector3 _firstAngle = default;

    // ����t���O�@�����������true�ɂ���
    private bool _isSetAngle = false;

    // ��̏����@�O���ɐi��ł����͂̑傫��
    // SetArrowSpeed�ő��x���w��ł���
    private float _arrowSpeed = 20f;


    // �萔

    // �[���@�����̃[���@���ʂɃ[���@�Ȃ�float �j���[�X�ԑg�ł͂Ȃ�
    private const float ZERO = 0f;

    #endregion

    #region �m�[�}���Ŏg�p���Ă���ϐ�

    // ��̑��x�̌v�Z��K�v�ȉ��z���x�@
    // �����̑��x�݂̂�z��I�Ɍv�Z����̂Ɏg��
    // �v�Z���F�@imageSpeed - (���Ԗ���AIR_RESIST�ɂ�錸���l)
    private float _imageSpeed = default;

    // �����~�����@���x�ɉ����ĕω����鉺�����ɂ�����͂̔{��
    // �v�Z���F�@FALL_MAX_VALUE �|�i���ݑ��x��FALLSPEEDLIMIT�j�@�@�@
    // �i FALL_MAX_VALUE �� fallValue �� �O �j
    private float _fallValue = default;

    // �^�����������߂ɕK�v�ȕ�Ԓl
    // fallValue�Ƃ������킹�邱�ƂŎ��Ԃ��Ƃ̊p�x�ω������߂���
    // firstAngle��FALL_MAX_VALUE�̍�
    private float _fallAngle = default;

    // �^���������p�x�@�w���������������ɂ���ĕω�������
    private float _underVecter = default;

    // �~��������x�@�~�����n�߂����l
    // ���̑��x�𒴂��Ă���Ԃ͍~�����Ȃ��ł܂��������
    // ���z���x���O�ɂȂ�����^�������ɐi��
    // �ȈՖ@���v�Z�ɂ悭�g����v�Z���@
    private float _fallStartSpeed = default;

    // ���݊p�x�@�V���{�����̔����������炷���߂Ɏg�p
    private float _nowAngle = default;

    // �V���{�����␳���x�̉��Z�l�@�ړ�������_arrowSpeed�ɉ��Z����                                                                           �����H�ׂ���
    private float _addFallSpeed = default;



    // �萔

    // �����~�����̍ő�l�@�P�@�����@one�@��������Ȃ�
    private const float FALL_MAX_VALUE = 1f;

    // ��C��R���@���x�����̑召�����߂�l�@�O�Ŗ���R �P�Ŗ��b�Pf����
    private const float AIR_RESIST = 5f;

    // �^���������p�x�@���ړ����Ȃ� �w�������̎��Ɏg�p
    private const float UNDER_VECTOR_POS = 90f;

    // �^���������p�x�@���ړ����Ȃ� �w�������̎��Ɏg�p
    private const float UNDER_VECTOR_NEG = 450f;

    // �V���{�����␳���x�@���̑��x���x���ꍇ�͉���������
    private const float ADD_FALL_SPEED_MAX = 100f;

    // �V���{�����␳�J�n�̊p�x
    private const float ADD_START_ANGLE = 90f;

    // �V���{�����␳�I���̊p�x�@Lerp�̏���ɂ��g��
    private const float ADD_FINISH_ANGLE = 180f;

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

    #endregion

    #endregion

    public void ArrowMove_Nomal(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Bomb(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Thunder(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBack(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Homing(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_Penetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombThunder(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombKnockBack(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_BombPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderKnockBack(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_ThunderPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBackHoming(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_KnockBackPenetrate(Transform arrowTransform) { NormalMove(arrowTransform, _arrowSpeed); }





    public void ArrowMove_HomingPenetrate(Transform arrowTransform) { HomingMove(arrowTransform, _arrowSpeed); }


    /// <summary>
    /// �ʏ�̖�̋����������郁�\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł������x</param>
    private void NormalMove(Transform arrowTransform ,float arrowSpeed)
    {
        #region ���ʂ̔�ѕ�
        // �����p�x�̑��
        if (!_isSetAngle)
        {
            // �����̊p�x�����[���h���W�̐��l�ő���@�F�X�g��
            _firstAngle = new Vector3(arrowTransform.eulerAngles.x, arrowTransform.eulerAngles.y, arrowTransform.eulerAngles.z);

            // �t���O�̐ݒ�@����������獡�������Ȃ��悤�ɕύX
            _isSetAngle = !_isSetAngle;
            
            // �p�x�␳�̕�Ԓl�v�Z
            // �����p�x���X�O�𒴂��Ă��邩�ǂ�������@����ɂ����_underVecter���ω�����
            if (_firstAngle.x > 90)
            {
                // �����p�x���X�O�𒴂��Ă���ꍇ�̒l����
                _underVecter = UNDER_VECTOR_NEG;
            }
            else
            {
                // �����p�x���X�O�𒴂��Ă��Ȃ��ꍇ�̒l����
                _underVecter = UNDER_VECTOR_POS;
            }
            
            // �X����K�v�̂���p�x���̂��̂̑��
            _fallAngle = _underVecter - _firstAngle.x;

            // ��̑��x��ݒ肷��
            _imageSpeed = arrowSpeed;
            _fallStartSpeed = _imageSpeed * 0.8f;
        }

        // ��̈ړ�
        arrowTransform.Translate(   ZERO,                                           // �w��
                                    ZERO,                                           // �x��
                                    (arrowSpeed + _addFallSpeed) * Time.deltaTime,  // �y��
                                    Space.Self);                                    // ���[�J�����W�Ŏw��@���͂y���Ɍ����Ă���O��

        // ���x����
        _imageSpeed = MathN.Clamp_min(_imageSpeed - (AIR_RESIST * Time.deltaTime), ZERO);

        // �����~�����v�Z
        _fallValue = MathN.Clamp( FALL_MAX_VALUE - (_imageSpeed / _fallStartSpeed), ZERO, FALL_MAX_VALUE);

        // �p�x�␳
        // ���݊p�x�ݒ�
        _nowAngle = _firstAngle.x + (_fallAngle * _fallValue);
        // �p�x���
        arrowTransform.rotation = Quaternion.Euler(_nowAngle,
                                                   _firstAngle.y,
                                                   _firstAngle.z);
        // �V���{�����␳
        if (arrowSpeed < ADD_FALL_SPEED_MAX && ADD_START_ANGLE <= _nowAngle && _nowAngle <= ADD_FINISH_ANGLE)
        {
            _addFallSpeed = Mathf.Lerp( arrowSpeed , ADD_FALL_SPEED_MAX , _nowAngle / ADD_FINISH_ANGLE) - arrowSpeed;
        }


        #endregion
    }

    /// <summary>
    /// �z�[�~���O�̋����������郁�\�b�h
    /// </summary>
    /// <param name="arrowTransform">��̃g�����X�t�H�[��</param>
    /// <param name="arrowSpeed">����ł����X�s�[�h</param>
    private void HomingMove(Transform arrowTransform, float arrowSpeed)
    {
        #region �z�[�~���O�̔�ѕ�
        // �����p�x�̑���@�^�[�Q�b�g���Ȃ��Ȃ����ꍇ�͍ēx���
        if (_target == null || _target == default)
        {
            // �����ݒ�ƃ^�[�Q�b�g�̑I��
            SetHomingTarget(arrowTransform,arrowSpeed);
        }


        // �^�[�Q�b�g�ւ̃x�N�g�����擾
        _lookVect = _target.transform.position - arrowTransform.position;
        // �ŏI�p�x��ݒ�
        _lookRot = Quaternion.LookRotation(_lookVect);
        // �I�u�W�F�N�g��rotation��ύX
        arrowTransform.rotation = Quaternion.Slerp(arrowTransform.rotation,_lookRot,_lookSpeed);

        // ��̈ړ�
        arrowTransform.Translate(   ZERO,                               // �w��
                                    ZERO,                               // �x��
                                    arrowSpeed * Time.deltaTime,        // �y��
                                    Space.Self);                        // ���[�J�����W�Ŏw��@����Z���Ɍ����Ă���O��
        #endregion
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
        _target = ConeDecision.ConeSearchNearest(arrowTransform, AttractObjectList.GetAttractObject(),SEARCH_ANGLE);


        // ��O�����@����������ɃI�u�W�F�N�g������Ȃ������ꍇ��nullRef��������邽�߂̏���
        if (_target == null || _target == default)
        {
            // �܂��������ōs���悤�Ƀ^�[�Q�b�g����
            _target = _tmpTarget;
        }
    }


    /// <summary>
    /// ��̑��x���w�肷��v���p�e�B�@set�����Ȃ����ߒ���
    /// </summary>
    public float SetArrowSpeed
    {
        set
        {
            _arrowSpeed = value/10;    
        }
    }

    /// <summary>
    /// ���X�^�[�g�ݒ�p�v���p�e�B
    /// </summary>
    public void ResetsStart()
    {
        _isSetAngle = false;
    }
}
