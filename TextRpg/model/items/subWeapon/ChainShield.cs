namespace TextRpg.model.items.subWeapon;

public class ChainShield : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.ChainShield;
    public string Name { get; } = "철방패";
    public string Description { get; } = "방어력이 증가합니다.";

    public string Effect1Desc => $"방어력 {30 + Enhancement * 8}";

    public string Effect2Desc => "";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 30 + Enhancement * 8);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.SubWeapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Rare;

    public Func<Character> OnEquip { get; }
}