// --------------------------------------------------------- 
// SceneFadeIn.cs 
// 
// CreateDay: 2023/06/24
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeIn : MonoBehaviour
{
    #region variable 
    [System.NonSerialized]
    public bool _isFadeEnd = false;
    private bool _isFade = false;

    protected float _alpha = default;//透過率、これを変化させる
    protected float _alphaEnd = default;
    protected const float ALPHA_STARTPERCENT_ZERO = 0.0f;
    protected const float ALPHA_STARTPERCENT_ONE = 1.0f;
    protected const float FADETIME = 2f;//フェードに掛かる時間

    private Image _myImage = default;
    private Color _myColor = new Color(0f, 0f, 0f, 0f);
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _myImage = this.GetComponentInChildren<Image>();
        _alphaEnd = StartAlphaColor();
    }

    private void Update()
    {
        SceneFade();
    }



    public void SceneFadeStart()
    {
        _isFade = true;
    }



    protected virtual float StartAlphaColor()
    {
        _alpha = ALPHA_STARTPERCENT_ZERO;
        _myColor.a = _alpha;
        _myImage.color = _myColor;
        return ALPHA_STARTPERCENT_ONE;
    }

    protected virtual bool IsAlphaEnd()
    {
        _alpha += Time.deltaTime / FADETIME;
        if (_alpha >= _alphaEnd)
        {
            return true;
        }
        return false;
    }

    private void SceneFade()
    {
        if (_isFade)
        {
            if (IsAlphaEnd())//透明になったら、フェードインを終了
            {
                _isFadeEnd = true;
            }
            _myColor.a = _alpha;
            _myImage.color = _myColor;
        }
    }
    #endregion
}