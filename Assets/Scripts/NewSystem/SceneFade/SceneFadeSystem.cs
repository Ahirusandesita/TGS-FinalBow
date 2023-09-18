// --------------------------------------------------------- 
// SceneFadeSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public interface ISceneFadeCallBack
{
    void SceneFadeInComplete();
    void SceneFadeOutComplete();
}

public static class FadeSystem
{
    public static SceneFadeSystem sceneFadeSystem;
    public static void SceneFadeInStart(this ISceneFadeCallBack sceneFadeCallBack) => sceneFadeSystem.SceneFadeInStart(sceneFadeCallBack);
    public static void SceneFadeOutStart(this ISceneFadeCallBack sceneFadeCallBack) => sceneFadeSystem.SceneFadeOutStart(sceneFadeCallBack);
}

public class SceneFadeSystem : MonoBehaviour
{
    #region variable 
    public Image fadeImage;
    private Color fadeColor;
    private WaitForSeconds fadeTime = new WaitForSeconds(0.025f);
    #endregion
    #region property
    #endregion
    #region method
    public void SceneFadeInStart(ISceneFadeCallBack sceneFadeCallBack) => StartCoroutine(FadeIn(sceneFadeCallBack));
    public void SceneFadeOutStart(ISceneFadeCallBack sceneFadeCallBack) => StartCoroutine(FadeOut(sceneFadeCallBack));
    private void Awake()
    {
        FadeSystem.sceneFadeSystem = this;
        fadeColor = fadeImage.color;
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;
    }
    private IEnumerator FadeIn(ISceneFadeCallBack sceneFadeCallBack)
    {
        while (true)
        {
            fadeColor.a -= 0.01f;
            if (fadeColor.a <= 0f)
            {
                fadeColor.a = 0f;
                fadeImage.color = fadeColor;
                sceneFadeCallBack.SceneFadeInComplete();
                break;
            }
            fadeImage.color = fadeColor;
            yield return fadeTime;
        }
    }

    private IEnumerator FadeOut(ISceneFadeCallBack sceneFadeCallBack)
    {
        while (true)
        {
            fadeColor.a += 0.01f;
            if(fadeColor.a > 1f)
            {
                fadeColor.a = 1f;
                fadeImage.color = fadeColor;
                sceneFadeCallBack.SceneFadeOutComplete();
                break;
            }
            fadeImage.color = fadeColor;
            yield return fadeTime;
        }
    }

    #endregion
}