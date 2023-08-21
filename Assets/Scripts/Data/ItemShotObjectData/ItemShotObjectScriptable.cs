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
    [SerializeField] public GameObject[] shotObjects = default;
    [SerializeField] public float rapidSpeed = 1f;
    [SerializeField] public Component physicsClassComponent = default;
    public IFItemShoterObjectPhysics itemPhysics { get; private set; }

    private void OnValidate()
    {
        if(physicsClassComponent.TryGetComponent<IFItemShoterObjectPhysics>(out IFItemShoterObjectPhysics physics))
        {
            itemPhysics = physics;
            Debug.Log(physics + "���Z�b�g");
        }
        else
        {
            Debug.Log(physicsClassComponent + "��IFItemShoterObjectPhysics���p�����Ă��܂���");
            physicsClassComponent = default;
        }
    }
}


