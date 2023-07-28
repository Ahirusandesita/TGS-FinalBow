// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v�[���I�u�W�F�N�g�̃p�����[�^��ݒ肵�ĕۑ�����N���X
/// </summary>
// Assets > Create > Scriptables > CreatePoolObjectTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "PoolObjectData", menuName = "Scriptables/CreatePoolObjectTable")]
public class PoolObjectParamTable : ScriptableObject
{
    // PoolInfomation�N���X�̓��e�����������X�g�𐶐�
    public List<PoolInformation> _scriptablePoolInformation = new List<PoolInformation>();

    // EffectInfomation�N���X�̓��e�����������X�g�𐶐�
    public List<EffectInformation> _scriptableEffectInformation = new List<EffectInformation>();
}

/// <summary>
/// �v�[���I�u�W�F�N�g�̏��
/// </summary>
// Inspector�ŕύX�����l���A�Z�b�g�Ƃ��ĕۑ������
[System.Serializable]
public class PoolInformation
{
    [Tooltip("�I�u�W�F�N�g��")]
    public string _name = default;
    [Tooltip("�C���X�^���X������v���n�u")]
    public CashObjectInformation _prefab = default;
    [Tooltip("�L���[�̍ő�e��")]
    public int _queueMax = default;
    [Tooltip("�v�[���I�u�W�F�N�g�̐������@")]
    public CreateType _createType = CreateType.automatic;
}

/// <summary>
/// �v�[���G�t�F�N�g�̎��
/// </summary>
[System.Serializable]
public class EffectInformation
{
    [Tooltip("�G�t�F�N�g��")]
    public string _name = default;

    [Tooltip("�C���X�^���X������G�t�F�N�g�v���n�u")]
    public GameObject _prefab = default;

    [Tooltip("�L���[�̍ő�e��")]
    public int _queueMax = default;
}