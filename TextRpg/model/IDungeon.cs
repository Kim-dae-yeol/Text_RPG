namespace TextRpg.model;

public interface IDungeon
{
    public string Desc()
    {
        //todo 줄정렬 맞추기 unicode 와 아스키 코드 구별해서 ~
        return $"| {Level} | + {RewardGold:C} | Def {RequireDefence} | Hp - {RequiresHp} | ";
    }

    public int RewardGold { get; }
    public string Level { get; }
    public int RequireDefence { get; }
    public int RequiresHp { get; }
}