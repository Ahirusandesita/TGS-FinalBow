// --------------------------------------------------------- 
// ProperaGimmickCallAction.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// 使い方
/// これはプロペラからActionを実行させるためのクラス
/// Actionを実行させたいオブジェクトにこれアタッチ
/// プロペラにこのオブジェクトを登録する
/// インターフェースを実装したクラスをアタッチした
/// オブジェクトを一つこのオブジェクトにアタッチする
/// 回すと動くーーー
/// </summary>
public class ProperaGimmickCallActionObject : MonoBehaviour, IFProperaLinkObject
{
    ProperaGimmick propera = default;

    bool needPower = true;

    [SerializeField] GameObject actionGimmick = default;

    IFGimmickCallerUsePower actionClassUsePower = default;

    IFGimmickCaller actionClass = default;

    [SerializeField] bool usePower = false;

    public bool GetNeedPower => needPower;

    public ProperaGimmick SetLinkObject { set => propera = value; }

    private void Start()
    {
        if (!usePower &&( actionClassUsePower is not null && actionClass is null))
        {
            actionClass = actionClassUsePower;
        }
    }
    private void OnValidate()
    {
        Init();
    }
    public void UsePowerAction(float power)
    {
        if (usePower)
        {
            actionClassUsePower.GimmickAction(power);
            needPower = !actionClassUsePower.IsFinish;
        }
        else
        {
            
            actionClass.GimmickAction();
            needPower = !actionClass.IsFinish;
        }
    }

    void Init()
    {
        if (needPower)
        {
            actionGimmick.TryGetComponent<IFGimmickCallerUsePower>(out actionClassUsePower);
        }
        else
        {
            actionGimmick.TryGetComponent<IFGimmickCaller>(out actionClass);
        }
    }
}