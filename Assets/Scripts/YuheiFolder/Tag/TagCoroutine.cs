// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// このクラスは使用しない
/// </summary>
public class TagCoroutine
{
    private Action _action;
    public TagCoroutine(Action action)
    {
        _action = action;
    }

    public IEnumerator TagSet()
    {
        yield return new WaitUntil(() => TagName.AddTags);
        _action();
    }
    public IEnumerator TagGet()
    {
        yield return new WaitUntil(() => TestAddTag.AddTag);
        _action();
    }
}
