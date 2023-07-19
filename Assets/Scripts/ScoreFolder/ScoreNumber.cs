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
    }



}