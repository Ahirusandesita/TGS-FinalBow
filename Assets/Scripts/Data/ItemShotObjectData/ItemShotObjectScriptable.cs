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
    [SerializeField] public GameObject[] shotObjects = default;
    [SerializeField] public float rapidSpeed = 1f;
    [SerializeField] public Component physicsClassComponent = default;
    public IFItemShoterObjectPhysics itemPhysics { get; private set; }

    private void OnValidate()
    {
        if(physicsClassComponent.TryGetComponent<IFItemShoterObjectPhysics>(out IFItemShoterObjectPhysics physics))
        {
            itemPhysics = physics;
            Debug.Log(physics + "をセット");
        }
        else
        {
            Debug.Log(physicsClassComponent + "はIFItemShoterObjectPhysicsを継承していません");
            physicsClassComponent = default;
        }
    }
}


