// 81-C# NomalScript-NewScript.cs
//
//CreateDay:2023/06/13
//Creator  :にゃーーーー
//
using UnityEngine;

interface ITargeterMove
{

}
public class TargeterMove : MonoBehaviour
{
    private Vector3 _spawnPosition = default;

    private ObjectPoolSystem PoolManager = default;
    private CashObjectInformation Cash = default;
    private ItemMove _itemMove = default;

    private void Start()
    {
        PoolManager = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        Cash = this.GetComponent<CashObjectInformation>();
        _itemMove = this.GetComponent<ItemMove>();
    }

    public void CreateTargeter(float distance_z, Transform goalTransform)
    {
        _spawnPosition = goalTransform.TransformVector(Vector3.forward) * distance_z;
        _spawnPosition = goalTransform.localPosition + _spawnPosition;
        _itemMove.SetTargeter = PoolManager.CallObject(PoolEnum.PoolObjectType.targeter, _spawnPosition).gameObject;
    }

    public void Re_setTargeter()
    {
        PoolManager.ReturnObject(Cash);
    }
}