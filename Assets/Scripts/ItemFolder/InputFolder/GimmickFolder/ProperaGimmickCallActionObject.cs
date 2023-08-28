// --------------------------------------------------------- 
// ProperaGimmickCallAction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// �g����
/// ����̓v���y������Action�����s�����邽�߂̃N���X
/// Action�����s���������I�u�W�F�N�g�ɂ���A�^�b�`
/// �v���y���ɂ��̃I�u�W�F�N�g��o�^����
/// �C���^�[�t�F�[�X�����������N���X���A�^�b�`����
/// �I�u�W�F�N�g������̃I�u�W�F�N�g�ɃA�^�b�`����
/// �񂷂Ɠ����[�[�[
/// </summary>
public class ProperaGimmickCallActionObject : MonoBehaviour, IFProperaLinkObject
{
    ProperaGimmick propera = default;

    bool needPower = true;

    [SerializeField] GameObject actionGimmick = default;

    IFGimmickCallerUsePower actionClassUsePower = default;

    IFGimmickCaller actionClass = default;

    [SerializeField] bool usePower = false;

    public bool GetNeedPower => needPower;

    public ProperaGimmick SetLinkObject { set => propera = value; }

    private void Start()
    {
        if (!usePower &&( actionClassUsePower is not null && actionClass is null))
        {
            actionClass = actionClassUsePower;
        }
    }
    private void OnValidate()
    {
        Init();
    }
    public void UsePowerAction(float power)
    {
        if (usePower)
        {
            actionClassUsePower.GimmickAction(power);
            needPower = !actionClassUsePower.IsFinish;
        }
        else
        {
            
            actionClass.GimmickAction();
            needPower = !actionClass.IsFinish;
        }
    }

    void Init()
    {
        if (needPower)
        {
            actionGimmick.TryGetComponent<IFGimmickCallerUsePower>(out actionClassUsePower);
        }
        else
        {
            actionGimmick.TryGetComponent<IFGimmickCaller>(out actionClass);
        }
    }
}