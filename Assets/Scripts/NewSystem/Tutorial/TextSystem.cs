// --------------------------------------------------------- 
// TextSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public interface ITextLikeSpeaking
{
    void IsComplete();
}

public class TextSystem : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = this.GetComponent<Text>();
        text.text = default;
    }

    public void TextLikeSpeaking(TutorialManagementData tutorialManagementData,ITextLikeSpeaking textLikeSpeaking)
    {
        StartCoroutine(LikeSpeaking(tutorialManagementData,textLikeSpeaking));
    }
    private IEnumerator LikeSpeaking(TutorialManagementData tutorialManagementData,ITextLikeSpeaking textLikeSpeaking)
    {

        for (int i = 0; i < tutorialManagementData.tutorialManagementItem.Count; i++)
        {
            TutorialData tutorialData = tutorialManagementData.tutorialManagementItem[i].tutorialDatas;
            WaitForSeconds waitForSeconds = new WaitForSeconds(tutorialData.speakingSpeed);
            int textCount = 0;
            for (int k = 0; k < tutorialData.text.Length; k++)
            {
                char textChar = tutorialData.text[k];
                string textString = default;
                textCount++;
                if (textChar == 'ÅB')
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
                    if (textChar == 'ÅA')
                    {
                        textString += "\n";
                        textCount = 0;
                    }
                }
                this.text.text += textString;
                yield return waitForSeconds;
            }
            yield return new WaitForSeconds(tutorialManagementData.tutorialManagementItem[i].nextTime);
            this.text.text = default;
        }
        textLikeSpeaking.IsComplete();

    }
}