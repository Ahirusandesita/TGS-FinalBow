// --------------------------------------------------------- 
// SceneFadeOut.cs 
// 
// CreateDay: 2023/06/24
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class SceneFadeOut : SceneFadeIn
{
    #region method


    protected override float StartAlphaColor()
    {
        _alpha = ALPHA_STARTPERCENT_ONE;
        return ALPHA_STARTPERCENT_ZERO;
    }

    protected override bool IsAlphaEnd()
    {
        _alpha -= Time.deltaTime / FADETIME;
        if (_alpha <= _alphaEnd)
        {
            return true;
        }
        return false;
    }
    #endregion
}