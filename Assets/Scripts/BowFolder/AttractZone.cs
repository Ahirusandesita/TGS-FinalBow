// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

public class AttractZone : MonoBehaviour
{
    /// 円錐のエイムエリアにAttractObjectListクラスのListが含まれていたらEnumをPlayerManagerにセットする
    #region 変数宣言部
    //アイテムクラス
    //public ItemStatus testItem;

    //ホールクラス　
    public Inhall inhallC;

    /// <summary>
    /// 円錐の角度倍率
    /// </summary>
    public float angleSizeMagnification = 100f;

    private float angle;
    /// <summary>
    /// 円錐の大きさ
    /// </summary>
    public float dictance;

    /// <summary>
    /// 円錐の方向　１or-1
    /// </summary>
    public int direction;

    /// <summary>
    /// 引き寄せる強さ
    /// </summary>
    public float attractPower;


    //アイテムインターフェース
    private IInhallDestroObject _inhall;
    //エリアに入っているGameObject用のリスト
    private List<GameObject> _zoneObject = new List<GameObject>();
    #endregion


    private void Start()
    {
        _inhall = inhallC;
    }

    private void Update()
    {
        // インプットからまたは弓から引き具合の数値をもらう
        //angle = 
        //ゾーンに含まれているオブジェクトのStatsEnumをPlayerManagerクラスにセットする
        
        if(angle > 180f)
        {
            angle = 180f;
        }

        _zoneObject = ConeDecision.ConeInObjects(transform, AttractObjectList.GetAttractObject(), angle, dictance, direction);
        for (int i = 0; i < _zoneObject.Count; i++)
        {
            //Update文だからおなじやつでも何回も呼ぶ　注意
            _inhall.SetGameObject(_zoneObject[i]);
        }
    }

    /// <summary>
    /// 吸込み角度に変換
    /// </summary>
    /// <param name="drawDistance">引いた距離</param>
    public void SetAngle(float drawDistance)
    {
       
        // ここになんか計算いれれば
        angle = drawDistance * angleSizeMagnification;

    }
}
