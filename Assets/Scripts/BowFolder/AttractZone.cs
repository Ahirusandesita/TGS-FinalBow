// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

public class AttractZone : MonoBehaviour
{
    /// �~���̃G�C���G���A��AttractObjectList�N���X��List���܂܂�Ă�����Enum��PlayerManager�ɃZ�b�g����
    #region �ϐ��錾��
    //�A�C�e���N���X
    //public ItemStatus testItem;

    //�z�[���N���X�@
    public Inhall inhallC;

    /// <summary>
    /// �~���̊p�x�{��
    /// </summary>
    public float angleSizeMagnification = 100f;

    private float angle;
    /// <summary>
    /// �~���̑傫��
    /// </summary>
    public float dictance;

    /// <summary>
    /// �~���̕����@�Por-1
    /// </summary>
    public int direction;

    /// <summary>
    /// �����񂹂鋭��
    /// </summary>
    public float attractPower;


    //�A�C�e���C���^�[�t�F�[�X
    private IInhallDestroObject _inhall;
    //�G���A�ɓ����Ă���GameObject�p�̃��X�g
    private List<GameObject> _zoneObject = new List<GameObject>();
    #endregion


    private void Start()
    {
        _inhall = inhallC;
    }

    private void Update()
    {
        // �C���v�b�g����܂��͋|���������̐��l�����炤
        //angle = 
        //�]�[���Ɋ܂܂�Ă���I�u�W�F�N�g��StatsEnum��PlayerManager�N���X�ɃZ�b�g����
        
        if(angle > 180f)
        {
            angle = 180f;
        }

        _zoneObject = ConeDecision.ConeInObjects(transform, AttractObjectList.GetAttractObject(), angle, dictance, direction);
        for (int i = 0; i < _zoneObject.Count; i++)
        {
            //Update�������炨�Ȃ���ł�������Ăԁ@����
            _inhall.SetGameObject(_zoneObject[i]);
        }
    }

    /// <summary>
    /// �z���݊p�x�ɕϊ�
    /// </summary>
    /// <param name="drawDistance">����������</param>
    public void SetAngle(float drawDistance)
    {
       
        // �����ɂȂ񂩌v�Z������
        angle = drawDistance * angleSizeMagnification;

    }
}
