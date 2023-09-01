// 81-C# FactoryScript-FactoryScript.cs
//
//CreateDay:
//Creator  :
//

using UnityEngine;
public interface IFItemShoterObjectPhysics
{
    /// <summary>
    /// �A�C�e���𓮂���
    /// </summary>
    void ItemMove();
    /// <summary>
    /// �������A�C�e����o�^����
    /// </summary>
    void ItemMoveStart(GameObject[] Items);
}
[CreateAssetMenu(fileName = "ItemShoterData", menuName = "Scriptables/ItemShoterData")]

public class ItemShotObjectScriptable : ScriptableObject
{
    [SerializeField] public PoolEnum.PoolObjectType objectType = PoolEnum.PoolObjectType.normalBullet;
    [SerializeField] public float rapidSpeed = 1f;

}


