// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//
using UnityEngine;
interface IItemShotDemo
{

}
public class ItemShotDemo : MonoBehaviour
{
    #region 
    [SerializeField]
    private GameObject _itemObject = default;

    [SerializeField]
    private Transform _spawner = default;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_itemObject,_spawner.position , Quaternion.identity);
        }
    }
    #endregion
}