// --------------------------------------------------------- 
// CashObjectInformation.cs 
// 
// CreateDay: 2023/06/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// �v�[���I�u�W�F�N�g�ɃA�^�b�`����A���L���b�V���p�N���X
/// </summary>
public class CashObjectInformation : MonoBehaviour
{
    [HideInInspector, Tooltip("���g�̃I�u�W�F�N�g�̎��")]
    public PoolEnum.PoolObjectType _myObjectType = default;
}