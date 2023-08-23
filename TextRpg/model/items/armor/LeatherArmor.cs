namespace TextRpg.model.items.armor;

public class LeatherArmor : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.LeatherArmor;
    public string Name => "천갑옷";
    public string Description => "방어력이 소폭 증가합니다.";

    public string Effect1Desc => $"방어력 +{20 + Enhancement * 10}";

    public string Effect2Desc => "";

    public string Effect3Desc => "";
    

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 20 + Enhancement * 10);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Armor;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Normal;

    public Func<Character> OnEquip { get; }
}