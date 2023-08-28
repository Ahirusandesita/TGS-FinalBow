// --------------------------------------------------------- 
// Button.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 使い方
/// とりあえずボタンにアタッチする
/// IFGimmickCallerの継承して実装しクラスをアタッチしたギミックを登録する
/// ボタンをおす
/// Action!!!!!
/// </summary>
public class ButtonGimmickCallAction : MonoBehaviour, IFCanTakeArrowButton
{
    [SerializeField] List<GameObject> gimmicks = new List<GameObject>();
    IFGimmickCaller[] callers = default;
    bool canStartCoroutine = true;
    
    private void Start()
    {
        Init();
    }

    private void OnValidate()
    {
        Init();
    }
    public void ButtonPush()
    {
        if(canStartCoroutine)
        StartCoroutine(Action());
    }

    
    IEnumerator Action()
    {
        canStartCoroutine = false;
        bool canRoop = true;
        while (canRoop)
        {
            canRoop = false;
            foreach (IFGimmickCaller gimmickCaller in callers)
            {
                if (gimmickCaller.Moving is false || gimmickCaller.IsFinish is false)
                {
                    gimmickCaller.GimmickAction();
                    canRoop = true;
                }
                
            }
            yield return null;
        }
        canStartCoroutine = true;
    }
    void Init()
    {
        
        SelectArray listIn = new SelectArray();
        if(gimmicks.Count == 0)
        {
            gimmicks.Add(this.gameObject);
        }
        GameObject[] objs = gimmicks.ToArray();
        callers = listIn.GetSelectedArray<IFGimmickCaller>(objs);
        gimmicks.Clear();
        gimmicks.AddRange(listIn.GetSelectedArrayReturnGameObjects<IFGimmickCaller>(objs));

        
    }

    GameObject IFCanTakeArrowButton.GetThisObject()
    {
        return gameObject;
    }
}