namespace TextRpg.model.items.weapon;

public class BloodyThirster : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.BloodThirster;
    public string Name { get; } = "피바라기";
    public string Description { get; } = "공격력이 비약적으로 상승합니다.";

    public string Effect1Desc => $"공격력 {100 + Enhancement * 50}";

    public string Effect2Desc => $"치명타 확률 {10 + Enhancement * 1}%";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Speed, 100 + Enhancement * 50);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Critical, 10 + Enhancement * 1);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Weapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Legendary;

    public Func<Character> OnEquip { get; }
}