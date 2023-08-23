namespace TextRpg.model.items.armor;

public class GargoyleArmor : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.GargoyleArmor;
    public string Name { get; } = "가고일 돌갑옷";
    public string Description { get; } = "방어력과 체력이 증가합니다.";

    public string Effect1Desc => $"방어력 {250 + Enhancement * 100}";

    public string Effect2Desc => $"체력 {450 + Enhancement * 100}";

    public string Effect3Desc => "";
    

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 250 + Enhancement * 100);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Hp, 450 + Enhancement * 100);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Armor;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Legendary;

    public Func<Character> OnEquip { get; }
}