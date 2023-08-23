namespace TextRpg.model.items.subWeapon;

public class StormShield : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.Stormshield;
    public string Name { get; } = "폭풍막이";
    public string Description { get; } = "방어력이 증가합니다";

    public string Effect1Desc => $"방어력 {200 + Enhancement * 100}";

    public string Effect2Desc => "";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 200 + Enhancement * 100);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.SubWeapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Legendary;

    public Func<Character> OnEquip { get; }
}