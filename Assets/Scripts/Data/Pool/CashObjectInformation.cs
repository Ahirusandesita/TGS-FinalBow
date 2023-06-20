// --------------------------------------------------------- 
// CashObjectInformation.cs 
// 
// CreateDay: 2023/06/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// プールオブジェクトにアタッチする、情報キャッシュ用クラス
/// </summary>
public class CashObjectInformation : MonoBehaviour
{
    [HideInInspector, Tooltip("自身のオブジェクトの種類")]
    public PoolEnum.PoolObjectType _myObjectType = default;
}