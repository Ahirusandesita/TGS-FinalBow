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
using TMPro;
using System.Collections.Generic;

public interface ITextLikeSpeaking
{
    void AllComplete();

    void ResponseComplete();

}
[RequireComponent(typeof(AudioSource))]
public class TextSystem : MonoBehaviour
{
    private TextMeshProUGUI text;
    public List<TMP_FontAsset> fontAssets = new List<TMP_FontAsset>();
    private float time;
    private const int MAX_RANDOM = 50;
    private AudioSource audioSource;

    Vector3 size;
    Vector3 defaultSize;

    private bool canNextText = false;

    private void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        audioSource = this.GetComponent<AudioSource>();
        text.text = default;
        defaultSize = transform.localScale;
        size = defaultSize * 1.006f;
    }

    private void Update()
    {
        //この機能を別クラスにする
        int nowint = default;
        int lastInt = default;

        //if (Random.Range(0, MAX_RANDOM) == 1)
        //{
        //    this.transform.localScale = size;
        //}
        //else
        //{
        //    this.transform.localScale = defaultSize;
        //}

        while (nowint == lastInt)
            nowint = Random.Range(0, fontAssets.Count - 1);
        lastInt = nowint;
        text.font = fontAssets[nowint];
    }

    public void TextLikeSpeaking(TutorialManagementData tutorialManagementData, ITextLikeSpeaking textLikeSpeaking)
    {
        StartCoroutine(LikeSpeaking(tutorialManagementData, textLikeSpeaking));
    }
    private IEnumerator LikeSpeaking(TutorialManagementData tutorialManagementData, ITextLikeSpeaking textLikeSpeaking)
    {

        for (int i = 0; i < tutorialManagementData.tutorialManagementItem.Count; i++)
        {
            if (!audioSource.isPlaying)
                if (tutorialManagementData.audioClip != null)
                    audioSource.PlayOneShot(tutorialManagementData.audioClip);

            TutorialData tutorialData = tutorialManagementData.tutorialManagementItem[i].tutorialDatas;
            WaitForSeconds waitForSeconds = new WaitForSeconds(tutorialData.speakingSpeed);
            if (tutorialData.speakingSpeed == 0f) canNextText = true;

            GameObject outImage = default;
            if (tutorialData.image != null)
            {
                outImage = Instantiate(tutorialData.image.gameObject, tutorialData.image.gameObject.transform.position, tutorialData.image.gameObject.transform.rotation);
            }

            if (i == tutorialManagementData.tutorialManagementItem.Count - 1) textLikeSpeaking.ResponseComplete();

            for (int k = 0; k < tutorialData.text.Length; k++)
            {

                string restText = default;
                if (tutorialData.text[k] == '<')
                {
                    string specialTag = tutorialData.text[k].ToString();
                    k++;
                    while (tutorialData.text[k] != '>')
                    {
                        specialTag += tutorialData.text[k];
                        k++;
                    }
                    k++;
                    specialTag += '>';
                    restText = specialTag;
                }

                restText += tutorialData.text[k];
                this.text.text += restText;
                if (!canNextText)
                    yield return waitForSeconds;

            }
            if (!tutorialData.canNextText)
                yield return new WaitForSeconds(tutorialManagementData.tutorialManagementItem[i].nextTime);
            else
            {
                StartNextTime();
                canNextText = false;
                yield return new WaitUntil(() => CanNextText || IsNextTime(tutorialManagementData.tutorialManagementItem[i].nextTime));
            }
            Destroy(outImage);
            outImage = null;
            this.text.text = default;
            canNextText = false;
        }
        textLikeSpeaking.AllComplete();

    }

    private void StartNextTime()
    {
        time = Time.time;
    }

    private bool IsNextTime(float nextTime) => nextTime == 0f ? false : Time.time - time > nextTime;
    public float A => Time.time - time;
    public void NextText() => canNextText = true;

    private bool CanNextText => canNextText;

}