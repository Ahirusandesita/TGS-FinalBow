// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

interface IInhall
{
    /// <summary>
    /// オブジェクト引き寄せる処理
    /// </summary>
    /// <param name="power">引き寄せの強さ</param>
    void InhallObject(GameObject obj);

    /// <summary>
    /// 敵引き寄せる処理
    /// </summary>
    /// <param name="power">引き寄せの強さ</param>
    void InhallEnemy(GameObject obj);

}
interface IInhallDestroObject
{
    /// <summary>
    /// 吸い込んで消滅させる処理
    /// </summary>
    /// <param name="obj">吸い込んだオブジェクト</param>
    void SetGameObject(GameObject obj);
}
public class Inhall : MonoBehaviour, IInhall, IInhallDestroObject
{
    #region 変数宣言部
    //PlayerManagerクラス　インターフェース
    private IFPlayerManagerEnchantParameter playerManager;

    //デバック用
    private ItemMove itemMove;
    public GameObject gollMovePosition;

    public float attractPower = 80f;

    public bool debugAttract = true;

    public TagObject _PlayerControllerTagData;
    #endregion


    private void Start()
    {
        try
        {
            //IPlayerManagerEnchantParameter型にPlayerManagerクラスを代入する
            playerManager = GameObject.FindGameObjectWithTag(_PlayerControllerTagData.TagName).GetComponent<PlayerManager>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("プレイヤーコントローラタグのオブジェクトが見つからない又は、PlayerManagerクラスがアタッチされていません");
        }
    }

    public void SetGameObject(GameObject obj)
    {

        GetComponent<BowVibe>().InhallStartVibe();


        if (obj.GetComponent<ItemStatus>().GetState() == EnchantmentEnum.ItemAttributeState.enemy)
        {
            InhallEnemy(obj);
        }
        else if (obj.GetComponent<ItemStatus>().GetState() == EnchantmentEnum.ItemAttributeState.obj)
        {
            InhallObject(obj);
        }
        else
        {
            //変更
            InhallItem(obj);
        }
        //　消滅処理オブジェクトのセットActivをFalseにする　ObjectPool

    }

    public void InhallEnemy(GameObject obj)
    {

    }

    public void InhallObject(GameObject obj)
    {
        ObjectParent objectParent = obj.GetComponent<ObjectParent>();
        objectParent.ObjectAction();
    }
    public void InhallItem(GameObject obj)
    {
        //PlayerManagerにArrowのparameterをセットする　属性
        //変更
        //obj.transform.parent = gollMovePosition.transform;

        itemMove = obj.GetComponent<ItemMove>();
        //itemMove.SetObj(gollMovePosition);
        obj.transform.rotation = Quaternion.identity;
        if (debugAttract)
        {
            itemMove.SetGoalPosition = gollMovePosition.transform;
        }
        else
        {
            obj.transform.parent = gollMovePosition.transform;
        }
        itemMove.SetAttractPower(attractPower);

        //デバック用
        itemMove.gameObject.TryGetComponent<IFItemMove>(out IFItemMove fItemMove);
        fItemMove.CanMove = false;
        itemMove._isStart = true;
        //itemMove.isStart = true;
        AttractObjectList.RemoveAttractObject(obj);

        //ついたら
        //playerManager.SetEnchantParameter(obj.GetComponent<ItemStatus>().GetState());
    }
}
