// --------------------------------------------------------- 
// EnemyMoveBase.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// �G���������N���X�̒�`
/// </summary>
public abstract class EnemyMoveBase : MonoBehaviour
{
    /// <summary>
    /// �e�E�F�[�u�̓G�̈�A�̋����i�C�x���g�Ƃ��Đi�s���܂Ƃ߂�j
    /// <para>Update�ŌĂ΂��</para>
    /// </summary>
    protected abstract void MoveSequence();
}