namespace TextRpg.model.items;

public class BestFriendSword : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.BestFriendSword;
    public string Name { get; } = "BF대검";
    public string Description { get; } = "공격력이 크게 증가합니다.";

    public string Effect1Desc => $"공격력 {50 + Enhancement * 10}";

    public string Effect2Desc => "";
    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Atk, 50 + Enhancement * 10);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Weapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Epic;

    public Func<Character> OnEquip { get; }
}