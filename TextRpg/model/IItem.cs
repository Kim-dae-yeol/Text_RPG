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
    public string Effect1Desc { get; }
    public string Effect2Desc { get; }
    public string Effect3Desc { get; }

    public KeyValuePair<ItemEffect, int>? Effect1 { get; }
    public KeyValuePair<ItemEffect, int>? Effect2 { get; }
    public KeyValuePair<ItemEffect, int>? Effect3 { get; }
    public int Enhancement { get; }
    internal ItemType Type { get; }
    internal ItemGrade Grade { get; }
    public Func<Character> OnEquip { get; }
    public static IItem Empty = new EmptyItem();

    private class EmptyItem : IItem
    {
        public int ID { get; } = (int)ItemIds.Empty;
        public string Name => "";
        public string Description => "";
        public string Effect1Desc => "";
        public string Effect2Desc => "";
        public string Effect3Desc => "";
        public KeyValuePair<ItemEffect, int>? Effect1 => null;
        public KeyValuePair<ItemEffect, int>? Effect2 => null;
        public KeyValuePair<ItemEffect, int>? Effect3 => null;
        public int Enhancement => 0;
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

    public enum ItemEffect
    {
        Atk,
        Speed,
        Hp,
        Critical,
        Unique
    }
}