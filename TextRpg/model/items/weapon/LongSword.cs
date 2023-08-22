namespace TextRpg.model.items.weapon;

public class LongSword : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.LongSword;
    public string Name { get; } = "롱소드";
    public string Description { get; } = "공격력이 상승합니다.";

    public string Effect1Desc => $"공격력 + {10 + Enhancement * 5}";

    public string Effect2Desc => "";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Atk, 10 + Enhancement * 5);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Weapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Normal;

    public Func<Character> OnEquip { get; }
}