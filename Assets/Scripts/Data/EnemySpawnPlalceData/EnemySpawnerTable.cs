// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �G�̃X�|�i�[��ݒ肵�ĕۑ�����N���X
/// </summary>
// Assets > Create > Scriptables > CreateEnemySpawnerTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Scriptables/CreateEnemySpawnerTable")]
public class EnemySpawnerTable : ScriptableObject
{
    [Header("�}�b�v���S�̍��W"), Tooltip("�}�b�v���S�̍��W")]
    public CentralInformation _centralInformation;
    [Header("�G���̏o���ʒu���X�g"), Tooltip("�G���̏o���ʒu���X�g")]
    public List<EnemySpawnerInformation> _scriptableESpawnerInformation = new List<EnemySpawnerInformation>();
}

/// <summary>
/// �G�̃X�|�i�[�̏��
/// </summary>
// Inspector�ŕύX�����l���A�Z�b�g�Ƃ��ĕۑ������
[System.Serializable]
public class EnemySpawnerInformation
{
    [Tooltip("�E�F�[�u��")]
    public string _wave;

    [Tooltip("�G���̃X�|�[���ʒu")]
    public List<Transform> _birdSpawnPlaces = new List<Transform>();

    [Tooltip("�G���̃S�[���ʒu")]
    public List<Transform> _birdGoalPlaces = new List<Transform>();
}

[System.Serializable]
public class CentralInformation
{
    public Transform _centralTransform;
}