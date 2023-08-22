using TextRpg.model.items;
using TextRpg.model.items.armor;
using TextRpg.model.items.weapon;

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

    public string Guid { get; set; }
    public int ID { get; }
    public string Name { get; }
    public string Description { get; }
    public string Effect1Desc { get; }
    public string Effect2Desc { get; }
    public string Effect3Desc { get; }

    public KeyValuePair<ItemEffect, int>? Effect1 { get; }
    public KeyValuePair<ItemEffect, int>? Effect2 { get; }
    public KeyValuePair<ItemEffect, int>? Effect3 { get; }
    public int Enhancement { get; set; }
    internal ItemType Type { get; }
    internal ItemGrade Grade { get; }
    public Func<Character> OnEquip { get; }
    public static IItem Empty = new EmptyItem();

    private class EmptyItem : IItem
    {
        public string Guid { get; set; } = "";
        public int ID => (int)ItemIds.Empty;
        public string Name => "";
        public string Description => "";
        public string Effect1Desc => "";
        public string Effect2Desc => "";
        public string Effect3Desc => "";
        public KeyValuePair<ItemEffect, int>? Effect1 => null;
        public KeyValuePair<ItemEffect, int>? Effect2 => null;
        public KeyValuePair<ItemEffect, int>? Effect3 => null;
        public int Enhancement { get; set; } = 0;
        public ItemType Type => ItemType.Nothing;
        public ItemGrade Grade => ItemGrade.Normal;

        //todo OnEquip 제거하기
        public Func<Character> OnEquip => null;
    }

    public enum ItemIds
    {
        Empty = 0,
        LongSword,
        PoisonDagger,
        BestFriendSword,
        BloodThirster,
        Guinsoo,
        LeatherArmor,
        ChainMail,
        ShapeOfSpirit,
        GargoyleArmor,
        HeartOfGiant,
        WoodenShield,
        ChainShield,
        PeltaLunata,
        Stormshield,
        SpiritWard,
        PearlNecklace, // rare
        DiamondNecklace, // epic
        AtmaScarab, // legendary
        HighlordWrath, // mythic
        
    }

    public enum ItemEffect
    {
        Atk,
        Speed,
        Hp,
        Critical,
        Defence,
        Unique
    }

    public static IItem FromString(string data)
    {
        var idAndEnhance = data.Split(":");
        var id = (ItemIds)int.Parse(idAndEnhance[0]);
        var enhance = int.Parse(idAndEnhance[1]);
        var gUid = idAndEnhance[2];
        var item = FromId(id);
        item.Guid = gUid;
        item.Enhancement = enhance;
        return item;
    }

    public static IItem FromId(ItemIds id)
    {
        return id switch
        {
            ItemIds.Empty => Empty,
            ItemIds.LongSword => new LongSword(),
            ItemIds.PoisonDagger => new LongSword(),
            ItemIds.BestFriendSword => new LongSword(),
            ItemIds.BloodThirster => new LongSword(),
            ItemIds.Guinsoo => new LongSword(),
            ItemIds.LeatherArmor => new LeatherArmor(),
            ItemIds.ChainMail => new ChainMail(),
            ItemIds.ShapeOfSpirit => new ShapeOfSpirit(),
            ItemIds.GargoyleArmor => new GargoyleArmor(),
            ItemIds.HeartOfGiant => new HeartOfGiant(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}