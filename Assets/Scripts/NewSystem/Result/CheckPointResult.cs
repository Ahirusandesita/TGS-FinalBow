// --------------------------------------------------------- 
// CheckPointResult.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CheckPointResult : MonoBehaviour
{
    #region variable 

    public List<GameObject> MeterObjects = new List<GameObject>();
    private List<Vector3> MeterSizes = new List<Vector3>();
    private List<RectTransform> MeterRectTransforms = new List<RectTransform>();

    public List<Sprite> ScoreRanks = new List<Sprite>();
    public Image ScoreRank = default;
    public Image LastScoreRank = default;

    public TextMeshProUGUI HitAverageText;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI ClearTimeText;
    public TextMeshProUGUI AttractGetValueText;
    public TextMeshProUGUI SumScoreText;

    public TextMeshProUGUI YouRankText;

    public GameObject PushObject;
    public TextMeshProUGUI PushMe;

    int sumScoreWork = default;

    [SerializeField]
    private float _waitTime = 0.3f;

    [SerializeField]
    private float _upSpeed = 5f;

    public struct ResultStruct
    {
        public float HitAverage;
        public int KillCount;
        public float ClearTime;
        public int AttractValue;
        public int SumScore;
    }
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        //text = this.GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < MeterObjects.Count; i++)
        {
            MeterRectTransforms.Add(MeterObjects[i].GetComponent<RectTransform>());
            MeterSizes.Add(MeterRectTransforms[i].localScale);
            MeterObjects[i].SetActive(false);
        }
        ScoreRank.sprite = ScoreRanks[0];
        LastScoreRank.sprite = ScoreRanks[0];
        YouRankText.text = default;
        PushObject.SetActive(false);
        PushMe.enabled = false;
    }

    public void Result(ref ResultStruct resultStruct)
    {
        HitAverageText.text = resultStruct.HitAverage.ToString() + "%";
        KillCountText.text = resultStruct.KillCount.ToString();
        ClearTimeText.text = resultStruct.ClearTime.ToString();
        AttractGetValueText.text = resultStruct.AttractValue.ToString();

        sumScoreWork = resultStruct.SumScore;
        SumScoreText.text = default;
        //SumScoreText.text = resultStruct.SumScore.ToString();

        StartCoroutine(MeterOutPut());
    }
    private void Update()
    {
        //int nowint = default;
        //int lastInt = default;
        //while (nowint == lastInt)
        //    nowint = Random.Range(0, fontAssets.Count - 1);

        //lastInt = nowint;

        //killCountText.font = fontAssets[nowint];
        //killScoreText.font = fontAssets[nowint];
        //numberOfCombosText.font = fontAssets[nowint];
        //clearTimeText.font = fontAssets[nowint];
        //sumScoreText.font = fontAssets[nowint];
    }

    private IEnumerator MeterOutPut()
    {

        WaitForSeconds fadeTime = new WaitForSeconds(0.025f);

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < MeterObjects.Count;)
        {
            MeterObjects[i].SetActive(true);
            MeterRectTransforms[i].localScale = MeterRectTransforms[i].localScale * 3f;

            for (; ; )
            {
                MeterRectTransforms[i].localScale /= 1.07f;
                if (MeterRectTransforms[i].localScale.x < MeterSizes[i].x)
                {
                    MeterRectTransforms[i].localScale = MeterSizes[i];
                    i++;
                    break;
                }
                yield return fadeTime;
            }
            yield return new WaitForSeconds(0.25f);
        }
        int textSumScore = default;

        yield return new WaitForSeconds(1f);
        WaitForSeconds waitSumScore = new WaitForSeconds(0.0001f);

        while (true)
        {
            textSumScore = textSumScore + 100;
            SumScoreText.text = textSumScore.ToString();
            if (textSumScore > sumScoreWork)
            {
                textSumScore = sumScoreWork;
                SumScoreText.text = textSumScore.ToString();
                break;
            }
            yield return waitSumScore;
        }

        yield return new WaitForSeconds(2f);

        int maxIndex = default;
        maxIndex = sumScoreWork < 10000 ? 1 : sumScoreWork < 20000 ? 2 : sumScoreWork < 30000 ? 3 : sumScoreWork < 40000 ? 4 : sumScoreWork < 50000 ? 5 : sumScoreWork < 60000 ? 6 : 7;


        for (int index = 1; index < maxIndex;)
        {
            ScoreRank.sprite = ScoreRanks[index];
            Vector3 size = ScoreRank.gameObject.transform.localScale;
            ScoreRank.gameObject.transform.localScale = ScoreRank.gameObject.transform.localScale * 3f;
            Color color = ScoreRank.color;
            color.a = 0f;

            for (; ; )
            {
                ScoreRank.gameObject.transform.localScale /= 1.07f;
                color.a += 0.03f;
                ScoreRank.color = color;
                if (ScoreRank.gameObject.transform.localScale.x <= size.x)
                {
                    ScoreRank.gameObject.transform.localScale = size;
                    color.a = 1f;
                    ScoreRank.color = color;
                    LastScoreRank.sprite = ScoreRanks[index];
                    index++;
                    break;
                }
                yield return fadeTime;

            }
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(2f);

        YouRankText.enabled = true;
        string rank = default;
        switch (maxIndex)
        {
            case 1: rank = "E"; break;
            case 2: rank = "D"; break;
            case 3: rank = "C"; break;
            case 4: rank = "B"; break;
            case 5: rank = "A"; break;
            case 6: rank = "S"; break;
            case 7: rank = "WA"; break;
        }
        YouRankText.text = rank;

        RectTransform youRankTextRectTransfrom = YouRankText.gameObject.GetComponent<RectTransform>();
        Vector3 youRankTextSize = youRankTextRectTransfrom.localScale;
        youRankTextRectTransfrom.localScale *= 3f;
        for (; ; )
        {
            youRankTextRectTransfrom.localScale /= 1.07f;
            if (youRankTextRectTransfrom.localScale.x < youRankTextSize.x)
            {
                youRankTextRectTransfrom.localScale = youRankTextSize;
                break;
            }
            yield return fadeTime;
        }

        yield return new WaitForSeconds(1f);

        WaitForSeconds pushTime = new WaitForSeconds(0.0125f);

        PushObject.SetActive(true);
        PushMe.enabled = true;

        Color pushMeColor = default;
        pushMeColor = PushMe.color;
        float alpha = 0f;
        bool isPlusAlpha = true;

        float plusValue = 0.01f;
        for (; ; )
        {
            if (isPlusAlpha)
            {
                alpha += plusValue;
                if(alpha >= 1f)
                {
                    alpha = 1f;
                    isPlusAlpha = false;
                }
            }
            else
            {
                alpha -= plusValue;
                if(alpha <= 0f)
                {
                    alpha = 0f;
                    isPlusAlpha = true;
                }
            }
            pushMeColor.a = alpha;
            PushMe.color = pushMeColor;

            yield return pushTime;
        }

        //ScoreRank.sprite = ScoreRanks[2];
        //LastScoreRank.sprite = ScoreRanks[2];
    }


    IEnumerator DestroyCount()
    {
        yield return new WaitForEndOfFrame();

    }
    #endregion
}