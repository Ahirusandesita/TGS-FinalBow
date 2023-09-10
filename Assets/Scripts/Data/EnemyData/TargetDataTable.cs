// --------------------------------------------------------- 
// TargetDataTable.cs 
// 
// CreateDay: 2023/08/30
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;

[CreateAssetMenu(fileName = "TargetData", menuName = "Scriptables/CreateTargetDataTable")]
public class TargetDataTable : ScriptableObject
{
    [Header("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Tooltip("�X�|�[���f�B���C�i���ӁFWave�J�n����̕b���j"), Min(0)]
    public float _spawnDelay_s;

    [Header("�X�|�[������ʒu"), Tooltip("�X�|�[������ʒu")]
    public Transform _spawnPlace;

    [Header("�I�𓮂���"), Tooltip("�I�𓮂���")]
    public bool _needMove;

    [HideInInspector, Tooltip("�S�[���̈ʒu")]
    public Transform _goalPlace;

    [HideInInspector, Tooltip("�ړ��X�s�[�h")]
    public float _speed;

    [HideInInspector, Tooltip("��~����")]
    public float _stayTime_s;
}