namespace TextRpg.model.items.subWeapon;

public class SpiritWard : IItem
{
    public string Guid { get; set; } = System.Guid.NewGuid().ToString();
    public int ID => (int)IItem.ItemIds.SpiritWard;
    public string Name { get; } = "스피릿 워드 ";
    public string Description { get; } = "방어력과 체력이 증가합니다. 방패로 때릴 수 있어서 공격력도 세집니다.";

    public string Effect1Desc => $"방어력 {450 + Enhancement * 200}";

    public string Effect2Desc => $"체력 {300 + Enhancement * 150}";

    public string Effect3Desc => $"공격력 {100 + Enhancement * 50}";

    public KeyValuePair<IItem.ItemEffect, int>? Effect1 => new(IItem.ItemEffect.Defence, 450 + Enhancement * 200);
    public KeyValuePair<IItem.ItemEffect, int>? Effect2 => new(IItem.ItemEffect.Hp, 300 + Enhancement * 150);
    public KeyValuePair<IItem.ItemEffect, int>? Effect3 => new(IItem.ItemEffect.Atk, 100 + Enhancement * 50);

    public int Enhancement { get; set; } = 0;

    public IItem.ItemType Type { get; } = IItem.ItemType.SubWeapon;
    public IItem.ItemGrade Grade { get; } = IItem.ItemGrade.Mythic;

    public Func<Character> OnEquip { get; }
}