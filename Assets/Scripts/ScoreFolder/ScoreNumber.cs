// --------------------------------------------------------- 
// ScoreNumber.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

public static class ScoreNumber
{
    public static Score ScorePoint;
    public struct Score
    {
        public int scoreNormalEnemy;
        public int scoreBossEnemy;
        public int scoreEnchant;
        public int scoreCoin;
        public int scoreComboBonus;

        public int scoreHpBonus;
        public int valueHpBonus;

        public int scoreAttractBonus;
        public int valueAttractBonus;

        public int scoreTimeBonus;
        public int valueTimeBonus;

        public int valueNormalEnemy;
        public int valueComboBonus;
        public static Score operator +(Score scoreA, Score scoreB)
        {
            scoreA.scoreNormalEnemy += scoreB.scoreNormalEnemy;
            scoreA.scoreBossEnemy += scoreB.scoreBossEnemy;
            scoreA.scoreEnchant += scoreB.scoreEnchant;
            scoreA.scoreCoin += scoreB.scoreCoin;
            scoreA.scoreComboBonus += scoreB.scoreComboBonus;
            scoreA.scoreHpBonus += scoreB.scoreHpBonus;
            scoreA.valueHpBonus += scoreB.valueHpBonus;
            scoreA.scoreAttractBonus += scoreB.scoreAttractBonus;
            scoreA.valueAttractBonus += scoreB.valueAttractBonus;
            scoreA.scoreTimeBonus += scoreB.scoreTimeBonus;
            scoreA.valueTimeBonus += scoreB.valueTimeBonus;

            scoreA.valueNormalEnemy += scoreB.valueNormalEnemy;
            scoreA.valueComboBonus += scoreB.valueComboBonus;

            return scoreA;
        }
        public void Reset()
        {
            scoreNormalEnemy = 0;
            scoreBossEnemy = 0;
            scoreEnchant = 0;
            scoreCoin = 0;
            scoreComboBonus = 0;

            scoreHpBonus = 0;
            valueHpBonus = 0;

            scoreAttractBonus = 0;
            valueAttractBonus = 0;

            scoreTimeBonus = 0;
            valueTimeBonus = 0;
            valueNormalEnemy = 0;
            valueComboBonus = 0;
        }
        //public int SumScore => scoreNormalEnemy + scoreBossEnemy + scoreEnchant + scoreCoin + scoreComboBonus + scoreHpBonus + valueHpBonus + scoreAttractBonus + valueAttractBonus + scoreTimeBonus + valueTimeBonus;
        public int SumScore => scoreNormalEnemy + scoreAttractBonus + scoreTimeBonus;
        



    }
        public static void ResetScore()
        {
            ScorePoint = new Score();
        }
}