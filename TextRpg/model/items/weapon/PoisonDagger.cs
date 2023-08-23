namespace TextRpg.model.items.weapon;

public class PoisonDagger : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.PoisonDagger;
    public string Name { get; } = "독이 발린 단검";
    public string Description { get; } = "공격력, 공격속도가 증가합니다.";

    public string Effect1Desc => $"공격 속도 {5 + Enhancement * 5}%";

    public string Effect2Desc => $"공격력 {10 + Enhancement * 5}";

    public string Effect3Desc => "";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Speed, 5 + Enhancement * 5);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Atk, 10 + Enhancement * 5);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => null;

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.Weapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Rare;

    public Func<Character> OnEquip { get; }
}