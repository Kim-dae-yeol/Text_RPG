namespace TextRpg.model.items.armor;

public class HeartOfGiant : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.HeartOfGiant;
    public string Name { get; } = "거인의 심장";
    public string Description { get; } = "방어력과 공격력과 체력이 증가합니다.";

    public string Effect1Desc => $"공격력 + {100 + Enhancement * 50}";

    public string Effect2Desc => $"방어력 {500 + Enhancement * 500}";

    public string Effect3Desc => $"체력 {600 + Enhancement * 400}";
    

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Atk, 100 + Enhancement * 50);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Defence, 500 + Enhancement * 500);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => new(IItem.ItemEffect.Hp, 600 + Enhancement * 400);

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Armor;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Mythic;

    public Func<Character> OnEquip { get; }
}