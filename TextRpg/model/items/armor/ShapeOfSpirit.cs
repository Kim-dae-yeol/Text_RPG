namespace TextRpg.model.items;

public class ShapeOfSpirit : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.ShapeOfSpirit;
    public string Name { get; } = "정령의 형상";
    public string Description { get; } = "방어력이 비약적으로 상승합니다.";

    public string Effect1Desc => $"방어력 {200 + Enhancement * 50}";

    public string Effect2Desc => "";

    public string Effect3Desc => "";


    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 200 + Enhancement * 50);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Weapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Mythic;

    public Func<Character> OnEquip { get; }
}