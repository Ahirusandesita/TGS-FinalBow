// 81-C# FactoryScript-FactoryScript.cs
//
//CreateDay:
//Creator  :
//

using UnityEngine;
public interface IFItemShoterObjectPhysics
{
    /// <summary>
    /// アイテムを動かす
    /// </summary>
    void ItemMove();
    /// <summary>
    /// 動かすアイテムを登録する
    /// </summary>
    void ItemMoveStart(GameObject[] Items);
}
[CreateAssetMenu(fileName = "ItemShoterData", menuName = "Scriptables/ItemShoterData")]

public class ItemShotObjectScriptable : ScriptableObject
{
    [SerializeField] public PoolEnum.PoolObjectType objectType = PoolEnum.PoolObjectType.normalBullet;
    [SerializeField] public float rapidSpeed = 1f;

}


