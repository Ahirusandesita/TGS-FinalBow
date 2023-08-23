// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

interface IInhall
{
    /// <summary>
    /// �I�u�W�F�N�g�����񂹂鏈��
    /// </summary>
    /// <param name="power">�����񂹂̋���</param>
    void InhallObject(GameObject obj);

    /// <summary>
    /// �G�����񂹂鏈��
    /// </summary>
    /// <param name="power">�����񂹂̋���</param>
    void InhallEnemy(GameObject obj);

}
interface IInhallDestroObject
{
    /// <summary>
    /// �z������ŏ��ł����鏈��
    /// </summary>
    /// <param name="obj">�z�����񂾃I�u�W�F�N�g</param>
    void SetGameObject(GameObject obj);
}
public class Inhall : MonoBehaviour, IInhall, IInhallDestroObject
{
    #region �ϐ��錾��
    //PlayerManager�N���X�@�C���^�[�t�F�[�X
    private IFPlayerManagerEnchantParameter playerManager;

    //�f�o�b�N�p
    private ItemMove itemMove;
    public GameObject gollMovePosition;

    public float attractPower = 80f;

    public bool debugAttract = true;

    public TagObject _PlayerControllerTagData;
    #endregion


    private void Start()
    {
        try
        {
            //IPlayerManagerEnchantParameter�^��PlayerManager�N���X��������
            playerManager = GameObject.FindGameObjectWithTag(_PlayerControllerTagData.TagName).GetComponent<PlayerManager>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("�v���C���[�R���g���[���^�O�̃I�u�W�F�N�g��������Ȃ����́APlayerManager�N���X���A�^�b�`����Ă��܂���");
        }
    }

    public void SetGameObject(GameObject obj)
    {

        GetComponent<BowVibe>().InhallStartVibe();


        if (obj.GetComponent<ItemStatus>().GetState() == EnchantmentEnum.ItemAttributeState.enemy)
        {
            InhallEnemy(obj);
        }
        else if (obj.GetComponent<ItemStatus>().GetState() == EnchantmentEnum.ItemAttributeState.obj)
        {
            InhallObject(obj);
        }
        else
        {
            //�ύX
            InhallItem(obj);
        }
        //�@���ŏ����I�u�W�F�N�g�̃Z�b�gActiv��False�ɂ���@ObjectPool

    }

    public void InhallEnemy(GameObject obj)
    {

    }

    public void InhallObject(GameObject obj)
    {
        ObjectParent objectParent = obj.GetComponent<ObjectParent>();
        objectParent.ObjectAction();
    }
    public void InhallItem(GameObject obj)
    {
        //PlayerManager��Arrow��parameter���Z�b�g����@����
        //�ύX
        //obj.transform.parent = gollMovePosition.transform;

        itemMove = obj.GetComponent<ItemMove>();
        //itemMove.SetObj(gollMovePosition);
        obj.transform.rotation = Quaternion.identity;
        if (debugAttract)
        {
            itemMove.SetGoalPosition = gollMovePosition.transform;
        }
        else
        {
            obj.transform.parent = gollMovePosition.transform;
        }
        itemMove.SetAttractPower(attractPower);

        //�f�o�b�N�p
        itemMove.gameObject.TryGetComponent<IFItemMove>(out IFItemMove fItemMove);
        fItemMove.CanMove = false;
        itemMove._isStart = true;
        //itemMove.isStart = true;
        AttractObjectList.RemoveAttractObject(obj);

        //������
        //playerManager.SetEnchantParameter(obj.GetComponent<ItemStatus>().GetState());
    }
}
