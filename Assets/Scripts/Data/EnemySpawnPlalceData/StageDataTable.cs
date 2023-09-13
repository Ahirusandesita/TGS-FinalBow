// --------------------------------------------------------- 
// EnemySpawnerTable.cs 
// 
// CreateDay: 2023/06/22
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// �X�e�[�W����ۑ�����N���X
/// </summary>
// Assets > Create > Scriptables > CreateEnemySpawnerTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "StageData", menuName = "Scriptables/CreateStageDataTable")]
public class StageDataTable : ScriptableObject
{
    [Tooltip("�E�F�[�u���X�g")]
    public List<WaveInformation> _waveInformation = new();
}

/// <summary>
/// �eWave�̓G�̏��
/// </summary>
// Inspector�ŕύX�����l���A�Z�b�g�Ƃ��ĕۑ������
[System.Serializable]
public class WaveInformation
{
    [Tooltip("Wave��")]
    public string _wave;

    [Header("Wave�J�n�f�B���C"), Tooltip("Wave�J�n�f�B���C")]
    public float _startDelay_s;

    [Header("���G���f�[�^"), Tooltip("���G���f�[�^")]
    public List<BirdDataTable> _birdsData = new();

    [Header("�n��G���f�[�^"), Tooltip("�n��G���f�[�^")]
    public List<GroundEnemyDataTable> _groundEnemysData = new();

    [Header("�I�̃f�[�^"), Tooltip("�I�̃f�[�^")]
    public List<TargetDataTable> _targetData = new();
}