// --------------------------------------------------------- 
// TextSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
public interface ITextLikeSpeaking
{
    void IsComplete();
}

public class TextSystem : MonoBehaviour
{
    private Text text;
    private float time;

    private bool canNextText = false;

    private void Awake()
    {
        text = this.GetComponent<Text>();
        text.text = default;
    }

    public void TextLikeSpeaking(TutorialManagementData tutorialManagementData, ITextLikeSpeaking textLikeSpeaking)
    {
        StartCoroutine(LikeSpeaking(tutorialManagementData, textLikeSpeaking));
    }
    private IEnumerator LikeSpeaking(TutorialManagementData tutorialManagementData, ITextLikeSpeaking textLikeSpeaking)
    {

        for (int i = 0; i < tutorialManagementData.tutorialManagementItem.Count; i++)
        {
            TutorialData tutorialData = tutorialManagementData.tutorialManagementItem[i].tutorialDatas;
            WaitForSeconds waitForSeconds = new WaitForSeconds(tutorialData.speakingSpeed);
            int textCount = 0;
            for (int k = 0; k < tutorialData.text.Length; k++)
            {
                if (canNextText)
                {
                    char restChar = tutorialData.text[k];
                    string restText = default;
                    textCount++;
                    if (restChar == '。')
                    {
                        textCount = 0;
                        restText = restChar + "\n";
                    }
                    else
                    {
                        restText = restChar.ToString();
                    }

                    if (textCount > 20)
                    {
                        if (restChar == '、')
                        {
                            restText += "\n";
                            textCount = 0;
                        }
                    }
                    this.text.text += restText;

                    break;
                }




                char textChar = tutorialData.text[k];
                string textString = default;
                textCount++;
                if (textChar == '。')
                {
                    textCount = 0;
                    textString = textChar + "\n";
                }
                else
                {
                    textString = textChar.ToString();
                }

                if (textCount > 20)
                {
                    if (textChar == '、')
                    {
                        textString += "\n";
                        textCount = 0;
                    }
                }
                this.text.text += textString;
                yield return waitForSeconds;
            }
            if (!tutorialData.canNextText)
                yield return new WaitForSeconds(tutorialManagementData.tutorialManagementItem[i].nextTime);
            else
            {
                StartNextTime();
                yield return new WaitUntil(() => CanNextText == true || IsNextTime(tutorialManagementData.tutorialManagementItem[i].nextTime) == true);
            }
            this.text.text = default;
            canNextText = false;
        }
        textLikeSpeaking.IsComplete();

    }

    private void StartNextTime()
    {
        time = Time.time;
    }

    private bool IsNextTime(float nextTime) => nextTime == 0f ? false : time + Time.time > nextTime;
    public void NextText() => canNextText = true;

    private bool CanNextText => canNextText;

}