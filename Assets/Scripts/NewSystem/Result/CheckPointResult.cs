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

    public AudioClip frameSE;
    public AudioClip scoreSE;
    public AudioClip rankFrameSE;
    public AudioClip rankSE;
    public AudioClip rankingSE;
    private AudioSource audioSource;


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


    public List<GameObject> TotalResultObjects = new List<GameObject>();
    public List<GameObject> RankingObjects = new List<GameObject>();
    public List<GameObject> RankingMeterObjects = new List<GameObject>();
    private List<RectTransform> RankingMeterTransform = new List<RectTransform>();
    private List<Vector3> RankingMeterSizes = new List<Vector3>();

    public List<TextMeshProUGUI> RankingScores = new List<TextMeshProUGUI>();
    public List<Image> RankingRanks = new List<Image>();
    public List<TextMeshProUGUI> RankingDays = new List<TextMeshProUGUI>();
    public Sprite NewRecordImage;

    public GameObject TotalResult;

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

    private bool isresultEnd = false;
    private bool isRankingOutput = false;
    private bool isRankingOutputEnd = false;

    private float startTime = 0f;
    private ResultStruct resultStruct;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();

        for (int i = 0; i < RankingObjects.Count; i++)
        {
            RankingObjects[i].SetActive(false);
        }
        for (int i = 0; i < RankingMeterObjects.Count; i++)
        {
            RankingMeterTransform.Add(RankingMeterObjects[i].GetComponent<RectTransform>());
            RankingMeterSizes.Add(RankingMeterTransform[i].localScale);
            RankingMeterObjects[i].SetActive(false);
        }
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
        TotalResult.SetActive(false);
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
        this.resultStruct = resultStruct;
        StartCoroutine(MeterOutPut());
    }
    public void ResultScreen() => StartCoroutine(MeterOutPut());

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

        if (OVRInput.Get(OVRInput.Button.Any))
        {
            if (isresultEnd)
            {
                isresultEnd = false;
                isRankingOutput = true;
                StartCoroutine(RankingOutput());
            }
        }

        if (isresultEnd && Time.time - startTime > 5f)
        {
            isresultEnd = false;
            isRankingOutput = true;
            StartCoroutine(RankingOutput());
        }
        if (isRankingOutputEnd && Time.time - startTime > 10f)
        {
            GameObject.FindObjectOfType<GameProgress>().ResultEnding();
        }

        if (Input.anyKeyDown)
        {
            if (isresultEnd)
            {
                isresultEnd = false;
                isRankingOutput = true;
                StartCoroutine(RankingOutput());
            }
        }



    }

    private IEnumerator MeterOutPut()
    {
        TotalResult.SetActive(true);
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
            if (frameSE is not null)
                audioSource.PlayOneShot(frameSE);
            yield return new WaitForSeconds(0.25f);
        }
        int textSumScore = default;

        yield return new WaitForSeconds(1f);
        WaitForSeconds waitSumScore = new WaitForSeconds(0.0001f);

        while (true)
        {
            if (!audioSource.isPlaying)
                if (scoreSE is not null)
                    audioSource.PlayOneShot(scoreSE);

            textSumScore = textSumScore + 300;
            SumScoreText.text = "<mspace=1em>" + textSumScore.ToString() + "</mspace>";
            if (textSumScore > sumScoreWork)
            {
                textSumScore = sumScoreWork;
                SumScoreText.text = "<mspace=1em>"+textSumScore.ToString()+"</mspace>";
                break;
            }
            yield return waitSumScore;
        }
        audioSource.Stop();

        yield return new WaitForSeconds(2f);

        int maxIndex = default;
        maxIndex = sumScoreWork < 60000 ? 1 : sumScoreWork < 75000 ? 2 : sumScoreWork < 85000 ? 3 : sumScoreWork < 100000 ? 4 : sumScoreWork < 120000 ? 5 : sumScoreWork < 140000 ? 6 : 7;


        for (int index = 1; index <= maxIndex;)
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
            if (rankFrameSE is not null)
                audioSource.PlayOneShot(rankFrameSE);

            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(2f);

        YouRankText.enabled = true;
        string rank = default;
        switch (maxIndex)
        {
            case 1: rank = "E"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "E"); break;
            case 2: rank = "D"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "D"); break;
            case 3: rank = "C"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "C"); break;
            case 4: rank = "B"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "B"); break;
            case 5: rank = "A"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "A"); break;
            case 6: rank = "S"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "S"); break;
            case 7: rank = "WS"; GameObject.FindObjectOfType<ScoreRankingSystem>().RankingUpdate(resultStruct.SumScore, "WS"); break;
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
        if (rankSE is not null)
            audioSource.PlayOneShot(rankSE);

        yield return new WaitForSeconds(1f);

        WaitForSeconds pushTime = new WaitForSeconds(0.0125f);

        PushObject.SetActive(true);
        PushMe.enabled = true;

        Color pushMeColor = default;
        pushMeColor = PushMe.color;
        float alpha = 0f;
        bool isPlusAlpha = true;

        float plusValue = 0.01f;
        isresultEnd = true;
        startTime = Time.time;
        for (; ; )
        {
            if (isPlusAlpha)
            {
                alpha += plusValue;
                if (alpha >= 1f)
                {
                    alpha = 1f;
                    isPlusAlpha = false;
                }
            }
            else
            {
                alpha -= plusValue;
                if (alpha <= 0f)
                {
                    alpha = 0f;
                    isPlusAlpha = true;
                }
            }
            pushMeColor.a = alpha;
            PushMe.color = pushMeColor;


            if (isRankingOutput) break;
            yield return pushTime;
        }

        //ScoreRank.sprite = ScoreRanks[2];
        //LastScoreRank.sprite = ScoreRanks[2];
    }

    private IEnumerator RankingOutput()
    {
        WaitForSeconds niceTime = new WaitForSeconds(0.025f);

        float rotateAngle = 0f;
        bool isRotate = true;
        while (true)
        {
            this.transform.Rotate(0f, 5f, 0f);
            rotateAngle += 5f;

            if (rotateAngle > 90f && isRotate)
            {
                isRotate = false;
                for (int i = 0; i < TotalResultObjects.Count; i++)
                {
                    TotalResultObjects[i].SetActive(false);
                }
                for (int i = 0; i < RankingObjects.Count; i++)
                {
                    RankingObjects[i].SetActive(true);
                }
            }

            if (rotateAngle >= 180f)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                break;
            }
            yield return niceTime;
        }

        List<ScoreRankingSystem.RankingElement> rankingElements = new List<ScoreRankingSystem.RankingElement>();
        rankingElements = GameObject.FindObjectOfType<ScoreRankingSystem>().GetRanking();
        ScoreRankingSystem scoreRankingSystem = GameObject.FindObjectOfType<ScoreRankingSystem>();
        for (int i = 0; i < rankingElements.Count;)
        {
            if (scoreRankingSystem.Record.isNewRecord)
                if (i == scoreRankingSystem.Record.newRecordIndex)
                {
                    //RankingMeterObjects[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = NewRecordImage;
                    StartCoroutine(NewRecordEffect(RankingMeterObjects[i]));
                }
            RankingScores[i].text = rankingElements[i].score + "pts";
            //RankingRanks[i].sprite 
            switch (rankingElements[i].rank)
            {
                case "E": RankingRanks[i].sprite = ScoreRanks[1]; break;
                case "D": RankingRanks[i].sprite = ScoreRanks[2]; break;
                case "C": RankingRanks[i].sprite = ScoreRanks[3]; break;
                case "B": RankingRanks[i].sprite = ScoreRanks[4]; break;
                case "A": RankingRanks[i].sprite = ScoreRanks[5]; break;
                case "S": RankingRanks[i].sprite = ScoreRanks[6]; break;
                case "WS": RankingRanks[i].sprite = ScoreRanks[7]; break;
            }
            RankingDays[i].text = rankingElements[i].day;
            RankingMeterObjects[i].SetActive(true);
            RankingMeterTransform[i].localScale = RankingMeterTransform[i].localScale * 3f;

            for (; ; )
            {
                RankingMeterTransform[i].localScale /= 1.07f;
                if (RankingMeterTransform[i].localScale.x < RankingMeterSizes[i].x)
                {
                    RankingMeterTransform[i].localScale = RankingMeterSizes[i];
                    i++;
                    break;
                }
                yield return niceTime;
            }
            if (rankingSE is not null)
                audioSource.PlayOneShot(rankingSE);
            yield return new WaitForSeconds(0.03f);
        }
        isRankingOutputEnd = true;
        startTime = Time.time;
    }

    private IEnumerator NewRecordEffect(GameObject meterObject)
    {
        while (true)
        {
            meterObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            meterObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
    }
    IEnumerator DestroyCount()
    {
        yield return new WaitForEndOfFrame();

    }


    #endregion
}