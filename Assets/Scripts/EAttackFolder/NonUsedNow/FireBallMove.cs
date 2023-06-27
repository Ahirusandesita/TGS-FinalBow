// --------------------------------------------------------- 
// FireBallMove.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class FireBallMove : EAttackMoveBase
{
    private void Start()
    {
        _transform = this.transform;

        // �{�X�̍U�����v�[������I�u�W�F�N�g����v�[�����擾
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        // Return�p�Ɏ������g�̏����擾
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        // �v���C���[���瓖���蔻��̃N���X���擾
        _playerHitZone = GameObject.FindWithTag(_PlayerControllerTagData.TagName).GetComponent<PlayerHitZone>();
    }

    private void OnEnable()
    {
        // �U���̐������Ԃ�ݒ�
        _lifeTime = 5f;

        // �t���O��������
        _canMove = true;
    }

    private void Update()
    {
        // �����Ȃ���ΕԂ�
        if (!_canMove)
        {
            return;
        }

        // �܂�������΂�
        Straight();

        // �v���C���[�Ƀq�b�g���Ă��邩�ǂ����𔻒�
        _playerHitZone.HitZone(_transform.position);

        // ���g�̎c�������Ԃ��f�N�������g
        _lifeTime -= Time.deltaTime;

        // �������Ԃ�0�ɂȂ��������
        if (_lifeTime <= 0f)
        {
            _objectPoolSystem.ReturnObject(_cashObjectInformation);
        }
    }
}
