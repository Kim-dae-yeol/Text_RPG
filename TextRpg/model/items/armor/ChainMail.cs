namespace TextRpg.model.items.armor;

public class ChainMail : IItem
{
    public string GUID { get; init; } = Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.Guinsoo;
    public string Name { get; } = "쇠사슬 갑옷";
    public string Description { get; } = "방어력이 증가합니다.";

    public string Effect1Desc => $"방어력 + {40 + Enhancement * 20}";

    public string Effect2Desc => "";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 40 + Enhancement * 20);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => null;
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Armor;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Rare;

    public Func<Character> OnEquip { get; }
}