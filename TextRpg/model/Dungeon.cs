namespace TextRpg.model;

public class Dungeon : IDungeon
{
    public int RewardGold { get; }
    public string Level { get; }
    public int RequireDefence { get; }
    public int RequiresHp { get; }

    public Dungeon(string level, int rewardGold, int requireDefence, int requiresHp)
    {
        Level = level;
        RewardGold = rewardGold;
        RequireDefence = requireDefence;
        RequiresHp = requiresHp;
    }

    public static Dungeon[] Dungeons =
    {
        new Dungeon(level: "쉬움", rewardGold: 3_000, requireDefence: 20, requiresHp: 10),
        new Dungeon(level: "보통", rewardGold: 10_000, requireDefence: 100, requiresHp: 100),
        new Dungeon(level: "악몽", rewardGold: 100_000, requireDefence: 1_000, requiresHp: 100),
        new Dungeon(level: "지옥", rewardGold: 1_000_000, requireDefence: 2_000, requiresHp: 100),
        new Dungeon(level: "불지옥", rewardGold: 5_000_000, requireDefence: 10_000, requiresHp: 100)
    };
}