namespace TextRpg.model.items.subWeapon;

public class PeltaLunata : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.PeltaLunata;
    public string Name { get; } = "펠타 루나타";
    public string Description { get; } = "방어력과 체력이 증가합니다.";

    public string Effect1Desc => $"방어력 {150 + Enhancement * 20}";

    public string Effect2Desc => $"체력 {200 + Enhancement * 100}";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 150 + Enhancement * 20);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Hp, 200 + Enhancement * 100);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.SubWeapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Epic;

    public Func<Character> OnEquip { get; }
}