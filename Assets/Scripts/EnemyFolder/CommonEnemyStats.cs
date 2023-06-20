// --------------------------------------------------------- 
// CommonEnemy.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

interface IFCommonEnemyGetParalysis
{
    bool Get_isParalysis { get; }

}
public abstract class CommonEnemyStats : EnemyStats,IFCommonEnemyGetParalysis
{
    protected Transform _myTransform = default;

    protected WaitForSeconds _paralysisWait = default;

    /// <summary>
    /// ��჎���
    /// </summary>
    protected readonly float _paralysisTime = 3f;

    /// <summary>
    /// �m�b�N�o�b�N����
    /// </summary>
    protected readonly float _knockBackDistance = 10f;

    /// <summary>
    /// �m�b�N�o�b�N�̋}�Ȍ������n�܂�̂����܂�(1�ȏ��)
    /// </summary>
    protected readonly float _knockBackDown = 1f;

    /// <summary>
    /// �^�C���A�E�g���鎞��
    /// </summary>
    protected readonly float _timeOutTime = 0.5f;

    /// <summary>
    /// ����HP
    /// </summary>
    protected readonly int _setHP = 30;

    protected bool _isParalysis = false;

    protected virtual void Start()
    {
        _paralysisWait = new WaitForSeconds(_paralysisTime);
        _myTransform = transform;
    }

    protected virtual void OnEnable()
    {
        // HP�̍Đݒ�
        _hp = _setHP;
    }


    public override void TakeBomb(int damage)
    {
        TakeDamage(damage);
        print("����������");
    }

    public override void TakeThunder()
    {
        StartCoroutine(ParalysisCoroutine());
    }

    public override void TakeKnockBack()
    {
        StartCoroutine(KnockbackCoroutine());
    }

    public override void Death()
    {
        print("���ɂ܂���");
        gameObject.SetActive(false);
        //_objPoolSys.ReturnObject(this.gameObject);
    }

    public override int HP
    {
        get
        {
            return _hp;
        }
    }

    public bool Get_isParalysis
    {
        get
        {
            return _isParalysis;
        }
    }


    /// <summary>
    /// �m�b�N�o�b�N����
    /// </summary>
    private IEnumerator KnockbackCoroutine()
    {
        // ���݂̃m�b�N�o�b�N���Ă��鋗��
        float nowKnockBackedDistance = 0;

        // �^�C���A�E�g�p
        float timeOut = Time.time + _timeOutTime;

        // ������������܂�
        while (_knockBackDistance > nowKnockBackedDistance)
        {
            // �ړ����鋗��
            float moveDistance = Mathf.Pow(_knockBackDistance * Time.deltaTime,
                (nowKnockBackedDistance / _knockBackDistance) * _knockBackDown);

            // �ړ����������ɉ��Z����
            nowKnockBackedDistance += moveDistance;

            // �ʒu�ύX
            _myTransform.Translate(Vector3.back * moveDistance);

            yield return null;

            if (Time.time > timeOut)
            {
                Debug.LogWarning("[test]�^�C���A�E�g");
                yield break;
            }
        }
    }

    /// <summary>
    /// �T���_�[�󂯂��Ƃ��̖�ჁH
    /// </summary>
    /// <returns></returns>
    private IEnumerator ParalysisCoroutine()
    {
        print("��გ�");
        _isParalysis = true;

        yield return _paralysisWait;

        print("��჉��");
        _isParalysis = false;
    }
}