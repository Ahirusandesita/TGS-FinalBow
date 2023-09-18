// --------------------------------------------------------- 
// ScoreRankingSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class ScoreRankingSystem : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method
    public struct NewRecord
    {
        public bool isNewRecord;
        public int newRecordIndex;
    }
    public NewRecord Record { get; private set; }

    public struct RankingElement
    {
        public string score;
        public string rank;
        public string day;
    }

    public void RankingUpdate(int score, string rank)
    {
        string filePath = Application.dataPath + "/Resources/ScoreRanking.txt";

        List<string> scores = new List<string>();
        List<string> ranks = new List<string>();
        List<string> days = new List<string>();
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string rankingKind = default;
                string line = default;
                while ((line = reader.ReadLine()) != null)
                {
                    int commaIndex = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ',')
                        {
                            commaIndex++;
                            if (commaIndex == 1)
                            {
                                scores.Add(rankingKind);
                            }
                            else if (commaIndex == 2)
                            {
                                ranks.Add(rankingKind);
                            }
                            else if (commaIndex == 3)
                            {
                                days.Add(rankingKind);
                                commaIndex = 0;
                            }
                            rankingKind = default;
                            continue;
                        }
                        rankingKind += line[i];
                    }
                }
            }

        }
        catch (IOException e)
        {
            Debug.LogError(e);
        }


        DateTime dt = DateTime.Now;
        string day = default;
        day += dt.Month + "/";
        day += dt.Day + "/";
        day += dt.Hour + ":";
        day += dt.Minute;



        if (scores.Count >= 10)
        {
            int minScore = 99999999;
            int index = default;
            for (int i = 0; i < scores.Count; i++)
            {
                int nowScore = int.Parse(scores[i]);
                if (nowScore < minScore)
                {
                    minScore = nowScore;
                    index = i;
                }
            }

            if (minScore < score)
            {
                scores[index] = score.ToString();
                ranks[index] = rank;
                days[index] = day;

                NewRecord record = default;
                record.isNewRecord = true;
                record.newRecordIndex = index;
                Record = record;
            }
        }
        else
        {
            scores.Add(score.ToString());
            ranks.Add(rank);
            days.Add(day);
            NewRecord record = default;
            record.isNewRecord = true;
            record.newRecordIndex = scores.Count - 1;
            Record = record;
        }

        for (int i = 0; i < scores.Count - 1; i++)
        {
            int nowScore = int.Parse(scores[i]);
            int index = default;
            bool canChenge = false;
            for (int k = i + 1; k < scores.Count; k++)
            {
                int nextScore = int.Parse(scores[k]);
                if (nowScore < nextScore)
                {
                    nowScore = nextScore;
                    index = k;
                    canChenge = true;
                }
            }

            if (canChenge)
            {
                string workScores = scores[i];
                string workRank = ranks[i];
                string workDays = days[i];

                scores[i] = scores[index];
                ranks[i] = ranks[index];
                days[i] = days[index];

                scores[index] = workScores;
                ranks[index] = workRank;
                days[index] = workDays;
                if (Record.isNewRecord)
                    if (Record.newRecordIndex == index)
                    {
                        NewRecord record = default;
                        record.isNewRecord = true;
                        record.newRecordIndex = i;
                        Record = record;
                    }
            }

        }


        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    writer.WriteLine(scores[i] + "," + ranks[i] + "," + days[i] + ",");
                }
                writer.Close();
            }
        }
        catch (IOException e)
        {
            Debug.LogError(e);
        }

    }

    public List<RankingElement> GetRanking()
    {
        string filePath = Application.dataPath + "/Resources/ScoreRanking.txt";

        List<string> scores = new List<string>();
        List<string> ranks = new List<string>();
        List<string> days = new List<string>();
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string rankingKind = default;
                string line = default;
                while ((line = reader.ReadLine()) != null)
                {
                    int commaIndex = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ',')
                        {
                            commaIndex++;
                            if (commaIndex == 1)
                            {
                                scores.Add(rankingKind);
                            }
                            else if (commaIndex == 2)
                            {
                                ranks.Add(rankingKind);
                            }
                            else if (commaIndex == 3)
                            {
                                days.Add(rankingKind);
                                commaIndex = 0;
                            }
                            rankingKind = default;
                            continue;
                        }
                        rankingKind += line[i];
                    }
                }
            }

        }
        catch (IOException e)
        {
            Debug.LogError(e);
        }

        List<RankingElement> rankingElements = new List<RankingElement>();
        RankingElement rankingElement = default;
        for (int i = 0; i < scores.Count; i++)
        {
            rankingElement.score = scores[i];
            rankingElement.rank = ranks[i];
            rankingElement.day = days[i];
            rankingElements.Add(rankingElement);
        }

        return rankingElements;
    }

    #endregion
}