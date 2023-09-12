// 81-C# FactoryScript-FactoryScript.cs
//
//CreateDay:
//Creator  :
//
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreFrameMaganer
{
    [SerializeField]
    private float _openSpeed = 5;

    [SerializeField]
    private float _closeSpeed = -5;

    [SerializeField]
    private float _maxWidth = 500f;

    [SerializeField]
    private float _maxHeight = 300f;

    [SerializeField]
    private Image _frameImage = default;

    [HideInInspector]
    public bool _endOpen = false;

    private float _nowWidthValue = 0.1f;

    private const float NOW_WIDTH_MIN = 0.1f;

    private const float NOW_WIDTH_MAX = 1f;

    private float _nowHeightValue = 0.1f;

    public void OpenFrame()
    {
        _frameImage.enabled = true;
    }

    IEnumerator OpenHeightCoroutine()
    {
        yield return new WaitForEndOfFrame();
        
    }

    IEnumerable OpenWidthCoroutine()
    {
        yield return new WaitForEndOfFrame();

    }
}
