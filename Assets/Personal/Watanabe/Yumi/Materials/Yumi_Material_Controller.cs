// --------------------------------------------------------- 
// Yumi_Material_Controller.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

interface IFMaterialChanger_Bow
{
    void ChangeMaterialProcess(EnchantmentEnum.EnchantmentState state);
}
/// <summary>
/// �G���`�����g���擾�����Ƃ��ɋ|�̃}�e���A����ύX����
/// ��I�u�W�F�N�g�Q
/// ��I�u�W�F�N�g�N���X�^���Q
/// ���I�u�W�F�N�g�Q
/// ���I�u�W�F�N�g�N���X�^���Q
/// ���[�^�[�P
/// ���[�^�[�Q
/// ���[�^�[�R
/// ���[�^�[�S
/// ���[�^�[�T
/// ����i�P�ڂ̃}�e���A���j
/// �����i�Q�ڂ̃}�e���A���j
/// �̏��ŕύX���Ă�
/// </summary>
public class Yumi_Material_Controller : MonoBehaviour,IFMaterialChanger_Bow
{
    /// <summary>
    /// �ォ��
    /// ��I�u�W�F�N�g�Q
    /// ��I�u�W�F�N�g�N���X�^���Q
    /// ���I�u�W�F�N�g�Q
    /// ���I�u�W�F�N�g�N���X�^���Q
    /// ���[�^�[�P
    /// ���[�^�[�Q
    /// ���[�^�[�R
    /// ���[�^�[�S
    /// ���[�^�[�T
    /// ����
    /// ����
    /// �̃}�e���A��
    /// </summary>
    #region �}�e���A�� 

    [Tooltip("�m�[�}��")]
    [SerializeField] List<Material> _normal = new List<Material>();
    [Tooltip("����")]
    [SerializeField] List<Material> _bomb = new List<Material>();
    [Tooltip("�T���_�[")]
    [SerializeField] List<Material> _thunder = new List<Material>();
    [Tooltip("�ђ�")]
    [SerializeField] List<Material> _penetrate = new List<Material>();
    [Tooltip("�z�[�~���O")]
    [SerializeField] List<Material> _horming = new List<Material>();
    [Tooltip("�A�i�U�[")]
    [SerializeField] List<Material> _another = new List<Material>();

    [Tooltip("�����T���_�[")]
    [SerializeField] List<Material> _bomb_thunder = new List<Material>();
    [Tooltip("�����ђ�")]
    [SerializeField] List<Material> _bomb_penetrate = new List<Material>();
    [Tooltip("�����z�[�~���O")]
    [SerializeField] List<Material> _bomb_horming = new List<Material>();
    [Tooltip("�����A�i�U�[")]
    [SerializeField] List<Material> _bomb_another = new List<Material>();
    [Tooltip("�T���_�[�ђ�")]
    [SerializeField] List<Material> _thunder_penetrate = new List<Material>();
    [Tooltip("�T���_�[�z�[�~���O")]
    [SerializeField] List<Material> _thunder_horming = new List<Material>();
    [Tooltip("�T���_�[�A�i�U�[")]
    [SerializeField] List<Material> _thunder_another = new List<Material>();
    [Tooltip("�ђʃz�[�~���O")]
    [SerializeField] List<Material> _penetrate_horming = new List<Material>();
    [Tooltip("�ђʃA�i�U�[")]
    [SerializeField] List<Material> _penetrate_another = new List<Material>();
    [Tooltip("�z�[�~���O�A�i�U�[")]
    [SerializeField] List<Material> _horming_another = new List<Material>();


    #endregion


    /// <summary>
    /// �㉺�I�u�W�F�N�g�͂P�ڂ̃}�e���A����ύX���Ă�
    /// ���͏㉺�i�P�C�Q�j�̃}�e���A�������ꂼ��ύX���Ă�
    /// </summary>
    #region �I�u�W�F�N�g

    [Tooltip("��I�u�W�F�N�g")]
    [SerializeField] List<MeshRenderer> _upObj = new List<MeshRenderer>();

    [Tooltip("��I�u�W�F�N�g�N���X�^��")]
    [SerializeField] List<MeshRenderer> _upObj_Crystal = new List<MeshRenderer>();

    [Tooltip("���I�u�W�F�N�g")]
    [SerializeField] List<MeshRenderer> _downObj = new List<MeshRenderer>();

    [Tooltip("���I�u�W�F�N�g�N���X�^��")]
    [SerializeField] List<MeshRenderer> _downObj_Crystal = new List<MeshRenderer>();

    [Tooltip("�z�����݃��[�^�[")]
    [SerializeField] List<MeshRenderer> _meter = new List<MeshRenderer>();

    [Tooltip("��")]
    [SerializeField] SkinnedMeshRenderer _gen = default;

    #endregion

    List<List<Material>> _materialsListLists = new List<List<Material>>();

    List<List<MeshRenderer>> _meshRenderersListLists = new List<List<MeshRenderer>>();

    #region method

    private void Start()
    {
        _materialsListLists = new List<List<Material>> { _normal,_bomb,_thunder,_penetrate,_horming,_another,_bomb_thunder,_bomb_penetrate,_bomb_horming,_bomb_another,
        _thunder_penetrate,_thunder_horming,_thunder_another,_penetrate_horming,_penetrate_another,_horming_another};

        // mater�͈��Ⴄ�}�e���A�����g�p���邽�ߏ��O
        _meshRenderersListLists = new List<List<MeshRenderer>> { _upObj, _upObj_Crystal, _downObj, _downObj_Crystal };
    }

    public void ChangeMaterialProcess(EnchantmentEnum.EnchantmentState state)
    {
        // �G���`�����g�ɂ���Ďg�p����list��ݒ�
        List<Material> setStateMaterials = _materialsListLists[(int)state];

        int index = -1;
        // ���b�V���O���[�v�𒊏o(obj�n)
        foreach (List<MeshRenderer> list in _meshRenderersListLists)
        {
            // �O���[�v�Ń}�e���A���ύX���I���ƃ}�e���A��list�̃C���f�b�N�X��1���₷
            // 3�őł��~�߂̂͂�
            index++;

            // �O���[�v�������b�V���I��
            foreach(MeshRenderer mesh in list)
            {
               
                //mesh.material = setStateMaterials[index];

            }

        }

        // meter�n�̃}�e���A���ύX�J�n
        foreach(MeshRenderer mesh in _meter)
        {
            index++;
            mesh.material = setStateMaterials[index];
        }

        // gen�̃}�e���A���ύX
        for(int i = 0; i < _gen.materials.Length;i++)
        {
            index++;
            _gen.materials[i] = setStateMaterials[index];
            
        }
    }
    #endregion
}