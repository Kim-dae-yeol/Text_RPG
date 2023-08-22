namespace TextRpg.model.items.armor;

public class GargoyleArmor : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.Guinsoo;
    public string Name { get; } = "구인수의 격노검";
    public string Description { get; } = "공격력, 치명타확률, 공격속도가 비약적으로 증가합니다.";

    public string Effect1Desc => $"공격 속도 {45 + Enhancement * 10}%";

    public string Effect2Desc => $"치명타 확률 {20 + Enhancement * 2}%";

    public string Effect3Desc => $"공격력 {60 + Enhancement * 20}";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Speed, 45 + Enhancement * 10);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Critical, 20 + Enhancement * 2);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => new(IItem.ItemEffect.Atk, 60 + Enhancement * 2);

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Weapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Mythic;

    public Func<Character> OnEquip { get; }
}