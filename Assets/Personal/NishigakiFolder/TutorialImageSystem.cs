// --------------------------------------------------------- 
// TutorialImageSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class TutorialImageSystem : MonoBehaviour
{
    #region variable 

    #region String
    [SerializeField]
    private Image _stringPointer = default;

    [SerializeField]
    private Color32 _stringPointerColor_1 = default;

    [SerializeField]
    private Color32 _stringPointerColor_2 = default;

    private float _stringTimer = default;

    private const float STRING_TIMELIMIT = 0.6f;
    #endregion

    #region guide
    [SerializeField]
    private Image _guide = default;

    [SerializeField]
    private Color32 _guideColor_1 = default;

    [SerializeField]
    private Color32 _guideColor_2 = default;

    private float _guideTimer = default;

    private const float GUIDE_TIMELIMIT = 0.6f;
    #endregion


    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        _stringPointer.enabled = false;
        _guide.enabled = false;
    }

    private void Update()
    {
        stringPointerEvent();
        guideEvent();
    }

    private delegate void StringPointerEvent();
    private delegate void GuideEvent();

    StringPointerEvent stringPointerEvent = null;
    GuideEvent guideEvent = null;

    public void StringPointerOpen()
    {
        _stringPointer.enabled = true;
        stringPointerEvent = StringPointerColorChenge_1;
    }

    private void StringPointerColorChenge_1()
    {
        _stringPointer.color = _stringPointerColor_1;

        _stringTimer += Time.deltaTime;

        if (_stringTimer > STRING_TIMELIMIT)
        {
            _stringTimer = 0f;
            stringPointerEvent = StringPointerColorChenge_2;
        }
    }

    private void StringPointerColorChenge_2()
    {
        _stringPointer.color = _stringPointerColor_2;

        _stringTimer += Time.deltaTime;

        if (_stringTimer > STRING_TIMELIMIT)
        {
            _stringTimer = 0f;
            stringPointerEvent = StringPointerColorChenge_1;
        }
    }

    public void StringPointerClose()
    {
        stringPointerEvent = null;
        _stringTimer = 0f;
        _stringPointer.enabled = false;
    }


    public void GuideOpen()
    {
        _guide.enabled = true;
        guideEvent = GuideColorChenge_1;
    }

    private void GuideColorChenge_1()
    {
        _guide.color = _guideColor_1;

        _guideTimer += Time.deltaTime;

        if (_guideTimer > GUIDE_TIMELIMIT)
        {
            _guideTimer = 0f;
            guideEvent = GuideColorChenge_2;
        }
    }

    private void GuideColorChenge_2()
    {
        _guide.color = _guideColor_2;

        _guideTimer += Time.deltaTime;

        if (_guideTimer > GUIDE_TIMELIMIT)
        {
            _guideTimer = 0f;
            guideEvent = GuideColorChenge_1;
        }
    }

    public void GuideClose()
    {
        guideEvent = null;
        _guideTimer = 0f;
        _guide.enabled = false;
    }

    #endregion
}