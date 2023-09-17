// --------------------------------------------------------- 
// MovieDisplayManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieDisplayManager : MonoBehaviour
{
    [HideInInspector]
    public bool _endOpen { get; set; }

    [HideInInspector]
    public bool _endClose { get; set; }

    [SerializeField]
    private float _openSpeed = 5;

    [SerializeField]
    private float _closeSpeed = -5;

    [SerializeField]
    private float _maxWidth = 500f;

    [SerializeField]
    private float _maxHeight = 300f;

    [SerializeField]
    private GameObject _frameObject = default;

    private RectTransform _frameTransform = default;

    private RawImage _displayRawImage = default;

    private Vector2 _nowScale = default;

    private float _nowWidthValue = NOW_WIDTH_MIN;

    private const float NOW_WIDTH_MIN = 0.02f;

    private const float NOW_WIDTH_MAX = 1f;

    private float _nowHeightValue = NOW_HEIGHT_MIN;

    private const float NOW_HEIGHT_MIN = 0.02f;

    private const float NOW_HEIGHT_MAX = 1f;

    private void Start()
    {
        _frameTransform = _frameObject.GetComponent<RectTransform>();

        _displayRawImage = _frameObject.GetComponent<RawImage>();
    }

    #region オープン
    public void OpenFrame()
    {
        _endOpen = false;
        _nowWidthValue = NOW_WIDTH_MIN;
        _nowHeightValue = NOW_HEIGHT_MIN;
        _nowScale = new Vector2(_nowWidthValue * _maxWidth, _nowHeightValue * _maxHeight);
        _displayRawImage.enabled = true;
        StartCoroutine(OpenHeightCoroutine());
    }

    IEnumerator OpenHeightCoroutine()
    {
        yield return new WaitForEndOfFrame();
        _nowHeightValue += _openSpeed * Time.deltaTime;
        if (_nowHeightValue < NOW_HEIGHT_MAX)
        {
            _nowScale.y = _nowHeightValue * _maxHeight;
            _frameTransform.sizeDelta = _nowScale;
            StartCoroutine(OpenHeightCoroutine());
        }
        else
        {
            _nowScale.y = NOW_HEIGHT_MAX * _maxHeight;
            StartCoroutine(OpenWidthCoroutine());
        }
    }

    IEnumerator OpenWidthCoroutine()
    {
        yield return new WaitForEndOfFrame();
        _nowWidthValue += _openSpeed * Time.deltaTime;
        if (_nowWidthValue < NOW_WIDTH_MAX)
        {
            _nowScale.x = _nowWidthValue * _maxWidth;
            _frameTransform.sizeDelta = _nowScale;
            StartCoroutine(OpenWidthCoroutine());
        }
        else
        {
            _nowScale.x = NOW_WIDTH_MAX * _maxWidth;
            _endOpen = true;
        }
    }
    #endregion

    #region クローズ

    public void CloseFrame()
    {
        _endClose = false;
        _nowWidthValue = NOW_WIDTH_MAX;
        _nowHeightValue = NOW_HEIGHT_MAX;
        _nowScale = new Vector2(_nowWidthValue * _maxWidth, _nowHeightValue * _maxHeight);
        StartCoroutine(CloseWidthCoroutine());
    }

    IEnumerator CloseWidthCoroutine()
    {
        yield return new WaitForEndOfFrame();
        _nowWidthValue += _closeSpeed * Time.deltaTime;
        if (_nowWidthValue > NOW_WIDTH_MIN)
        {
            _nowScale.x = _nowWidthValue * _maxWidth;
            _frameTransform.sizeDelta = _nowScale;
            StartCoroutine(CloseWidthCoroutine());
        }
        else
        {
            _nowScale.x = NOW_WIDTH_MIN * _maxWidth;
            StartCoroutine(CloseHeightCoroutine());
        }
    }

    IEnumerator CloseHeightCoroutine()
    {
        yield return new WaitForEndOfFrame();
        _nowHeightValue += _closeSpeed * Time.deltaTime;
        if (_nowHeightValue > NOW_HEIGHT_MIN)
        {
            _nowScale.y = _nowHeightValue * _maxHeight;
            _frameTransform.sizeDelta = _nowScale;
            StartCoroutine(CloseHeightCoroutine());
        }
        else
        {
            _nowScale.y = NOW_HEIGHT_MIN * _maxHeight;
            _displayRawImage.enabled = false;
            _endClose = true;
        }
    }
    #endregion

}