namespace TextRpg.model;

public interface IDungeon
{
    public string Desc()
    {
        //todo 줄정렬 맞추기 unicode 와 아스키 코드 구별해서 ~
        // todo 확장함수로 바꾸면 더 편할듯?

        var levelString = Level;
        var levelLen = Level.Length;
        for (var i = 0; i < 6 - levelLen; i++)
        {
            levelString += "  ";
        }

        var rewardGoldString = RewardGold.ToString("C");
        var rewardGoldStringLen = rewardGoldString.Length;
        var blankOfReward = "";
        for (var i = 0; i < 8 - rewardGoldStringLen; i++)
        {
            blankOfReward += "  ";
        }

        return
            $"{$"| {levelString} |",6}" +
            $" + {$"{blankOfReward}{RewardGold:C} |",15}" +
            $"{$" Def {RequireDefence} |",15}" +
            $"{$" Hp - {RequiresHp} | ",15}";
    }

    public int RewardGold { get; }
    public string Level { get; }
    public int RequireDefence { get; }
    public int RequiresHp { get; }

    public bool IsClear(Character c)
    {
        var isClear = false;
        var diffDefence = RequireDefence - c.Defence;
        if (diffDefence > 0)
        {
            //(30)*(float)(RequireDefence - c.Defence)확률로  clear
            var count = (int)30f * (diffDefence);
            isClear = Random.Shared.Next(0, count) <= 10;
        }
        else
        {
            // 95% clear
            isClear = Random.Shared.Next(1, 101) <= 90;
        }

        return isClear;
    }
}