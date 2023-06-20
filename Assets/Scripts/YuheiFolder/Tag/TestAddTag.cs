// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;
/// <summary>
/// ���̃N���X�͎g�p���Ȃ�
/// </summary>
public class TestAddTag : MonoBehaviour
{
    public static bool AddTag = false;
    public int a;
    private void Awake()
    {
        AddTag = false;
        Action action = () =>
        {
            this.gameObject.tag = TagName.tagName[a];
            AddTag = true;
        };
        StartCoroutine(new TagCoroutine(action).TagSet());
    }
}
