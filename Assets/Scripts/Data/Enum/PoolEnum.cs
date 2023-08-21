// --------------------------------------------------------- 
// PoolEnum.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class PoolEnum : MonoBehaviour
{
    /// <summary>
    /// �v�[���������I�u�W�F�N�g
    /// </summary>
    public enum PoolObjectType
    {
        /// <summary>
        /// ��
        /// </summary>
        arrow,
        /// <summary>
        /// �ʏ�̒e
        /// </summary>
        normalBullet,
        /// <summary>
        /// �����̒e
        /// </summary>
        bombBullet,
        /// <summary>
        /// �ђʂ̒e
        /// </summary>
        penetrateBullet,
        /// <summary>
        /// �d���̒e
        /// </summary>
        thunderBullet,
        /// <summary>
        /// ���̎G��
        /// </summary>
        bird,
        /// <summary>
        /// �A�C�e���̈����񂹗p
        /// </summary>
        targeter,
        /// <summary>
        /// �I
        /// </summary>
        targetObject,
        /// <summary>
        /// �m�[�}���U�R��
        /// </summary>
        normalBird,
        /// <summary>
        /// ���e�U�R��
        /// </summary>
        bombBird,
        /// <summary>
        /// �ђʃU�R��
        /// </summary>
        penetrateBird,
        /// <summary>
        /// ���U�R��
        /// </summary>
        thunderBird,
        /// <summary>
        /// �ł������e�U�R��
        /// </summary>
        bombBirdBig,
        /// <summary>
        /// �ł������U�R��
        /// </summary>
        thunderBirdBig,
        /// <summary>
        /// �ł����ђʃU�R��
        /// </summary>
        penetrateBirdBig,
        /// <summary>
        /// �n��̓G
        /// </summary>
        groundEnemy,
        /// <summary>
        /// ��������e
        /// </summary>
        groundBullet,
        /// <summary>
        /// �h���b�v�A�C�e��1
        /// </summary>
        dropItem_1,
    }
}
