namespace TextRpg.model;

public interface IItem
{
    public enum ItemType
    {
        Weapon,
        Helm,
        SubWeapon,
        Armor,
        Ring,
        Necklace,
        Nothing
    }

    public enum ItemGrade
    {
        Normal,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    public int ID { get; }
    public string Name { get; }
    public string Description { get; }
    public string Effect1 { get; }
    public string Effect2 { get; }
    public string Effect3 { get; }
    internal ItemType Type { get; }
    internal ItemGrade Grade { get; }
    public Func<Character> OnEquip { get; }
    public static IItem Empty = new EmptyItem();

    private class EmptyItem : IItem
    {
        public int ID { get; } = (int)ItemIds.Empty;
        public string Name => "";
        public string Description => "";
        public string Effect1 => "";
        public string Effect2 => "";
        public string Effect3 => "";
        public ItemType Type => ItemType.Nothing;
        public ItemGrade Grade => ItemGrade.Normal;
        public Func<Character> OnEquip => null;
    }

    public enum ItemIds
    {
        Empty = 0,
        Guinsoo,
        LongSword,
        Dagger,
    }
}