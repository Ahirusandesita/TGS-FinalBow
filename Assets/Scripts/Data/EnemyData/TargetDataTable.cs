// --------------------------------------------------------- 
// TargetDataTable.cs 
// 
// CreateDay: 2023/08/30
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TargetData", menuName = "Scriptables/CreateTargetDataTable")]
public class TargetDataTable : ScriptableObject
{
    [Header("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Tooltip("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j")]
    public float _spawnDelay_s;

    [Header("�X�|�[������ʒu"), Tooltip("�X�|�[������ʒu")]
    public Transform _spawnPlace;

    [Header("�����̎��"), Tooltip("�����̎��")]
    public MoveType _moveType;

    [Header("���[�v�̎�ށiforward�F���s,�@reverse�F�t�s�j"), Tooltip("���[�v�̎��")]
    public RoopType _roopType;

    [Header("�I�̋����̏ڍ׃��X�g"), Tooltip("�I�̋����̏ڍ׃��X�g")]
    public List<TargetMoveInformation> _targetMoveInformations = new();
}

[System.Serializable]
public class TargetMoveInformation
{
    [Header("�S�[���̈ʒu"), Tooltip("�S�[���̈ʒu")]
    public Transform _goalPlace;

    [Header("�ړ��X�s�[�h"), Tooltip("�ړ��X�s�[�h")]
    public float _speed;

    [Header("��~����"), Tooltip("��~����")]
    public float _stayTime_s;
}