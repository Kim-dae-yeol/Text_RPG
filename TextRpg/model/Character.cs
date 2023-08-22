using System.Text;
using TextRpg.model.items;
using TextRpg.model.items.armor;
using TextRpg.model.items.weapon;
using TextRpg.screens.status;

namespace TextRpg.model;

public class Character
{
    public string Name = "GIGA Chad";
    public string Job = "전사";
    public Equipment Equipment = new();
    public int Level = 1;
    public int Exp = 0;
    public int LevelUpExp = 100;
    public int Hp = 100;
    public int Defence = 30;
    public int Atk = 22;
    public float Speed = 1.0f;
    public int Critical = 0;
    private List<IItem> _inventory = new();
    public IReadOnlyList<IItem> Inventory => _inventory;
    public int Gold = 1500;

    public Character()
    {
        _inventory.Add(new LongSword());
        _inventory.Add(new LeatherArmor());
        for (var i = 2; i < 15; i++)
        {
            _inventory.Add(IItem.Empty);
        }
    }

    public Dictionary<IItem.ItemEffect, int> AddedItemEffects()
    {
        var items = Equipment
            .EquipItems()
            .Where(it => it != IItem.Empty);

        var dict = new Dictionary<IItem.ItemEffect, int>();

        foreach (var item in items)
        {
            if (item.Effect1 != null)
            {
                var pair = item.Effect1.Value;
                dict.Add(pair.Key, pair.Value);
            }

            if (item.Effect2 != null)
            {
                var pair = item.Effect2.Value;
                dict.Add(pair.Key, pair.Value);
            }

            if (item.Effect3 != null)
            {
                var pair = item.Effect3.Value;
                dict.Add(pair.Key, pair.Value);
            }
        }

        return dict;
    }

    public void EquipItem(IItem item)
    {
        var type = item.Type;
        switch (type)
        {
            case IItem.ItemType.Weapon:
                UnEquipItem(Equipment.Weapon);
                Equipment.Weapon = item;
                break;
            case IItem.ItemType.Helm:
                UnEquipItem(Equipment.Helm);
                Equipment.Helm = item;
                break;
            case IItem.ItemType.SubWeapon:
                UnEquipItem(Equipment.SubWeapon);
                Equipment.SubWeapon = item;
                break;
            case IItem.ItemType.Armor:
                UnEquipItem(Equipment.Armor);
                Equipment.Armor = item;
                break;
            case IItem.ItemType.Ring:
                if (Equipment.Ring1 != IItem.Empty && Equipment.Ring2 == IItem.Empty)
                {
                    Equipment.Ring2 = item;
                }
                else
                {
                    UnEquipItem(Equipment.Ring1);
                    Equipment.Ring1 = item;
                }

                break;
            case IItem.ItemType.Necklace:
                UnEquipItem(Equipment.Necklace);
                Equipment.Necklace = item;
                break;
            case IItem.ItemType.Nothing:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void EquipItemToSlot(IItem item, StatusScreen.EquipmentSlotType? slot)
    {
        if (slot == null) return;
        switch (slot)
        {
            case StatusScreen.EquipmentSlotType.Helm:
                Equipment.Helm = item;
                break;
            case StatusScreen.EquipmentSlotType.Necklace:
                Equipment.Necklace = item;
                break;
            case StatusScreen.EquipmentSlotType.Weapon:
                Equipment.Weapon = item;
                break;
            case StatusScreen.EquipmentSlotType.Armor:
                Equipment.Armor = item;
                break;
            case StatusScreen.EquipmentSlotType.SubWeapon:
                Equipment.SubWeapon = item;
                break;
            case StatusScreen.EquipmentSlotType.Ring1:
                Equipment.Ring1 = item;
                break;
            case StatusScreen.EquipmentSlotType.Ring2:
                Equipment.Ring2 = item;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }


    public void UnEquipItem(IItem? item)
    {
        if (item == null || item == IItem.Empty || !IsEquipped(item)) return;

        switch (item.Type)
        {
            case IItem.ItemType.Weapon:
                Equipment.Weapon = IItem.Empty;
                break;
            case IItem.ItemType.Helm:
                Equipment.Helm = IItem.Empty;
                break;
            case IItem.ItemType.SubWeapon:
                Equipment.SubWeapon = IItem.Empty;
                break;
            case IItem.ItemType.Armor:
                Equipment.Armor = IItem.Empty;
                break;
            case IItem.ItemType.Ring:
                if (Equipment.Ring1.Guid == item.Guid)
                {
                    Equipment.Ring1 = IItem.Empty;
                }
                else
                {
                    Equipment.Ring2 = IItem.Empty;
                }

                break;
            case IItem.ItemType.Necklace:
                Equipment.Necklace = IItem.Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ThrowItem(IItem item)
    {
        var index = _inventory.FindIndex(it => it == item);
        UnEquipItem(item);
        _inventory[index] = IItem.Empty;
    }

    public bool IsEquipped(IItem item)
    {
        return Equipment.EquipItems().Find(it => it.Guid == item.Guid) != null;
    }

    public static Character FromCsv(string? csvLine)
    {
        if (string.IsNullOrEmpty(csvLine))
        {
            return new Character();
        }

        var data = csvLine.Split(",");
        var c = new Character();
        c.Name = data[0];
        c.Job = data[1];
        c.Level = int.Parse(data[2]);
        c.Exp = int.Parse(data[3]);
        c.LevelUpExp = int.Parse(data[4]);
        c.Hp = int.Parse(data[5]);
        c.Atk = int.Parse(data[6]);
        c.Defence = int.Parse(data[7]);
        c.Critical = int.Parse(data[8]);
        c.Speed = int.Parse(data[9]);
        c.Gold = int.Parse(data[10]);

        c.Equipment.Helm = IItem.FromString(data[11]);
        c.Equipment.Armor = IItem.FromString(data[12]);
        c.Equipment.Necklace = IItem.FromString(data[13]);
        c.Equipment.Weapon = IItem.FromString(data[14]);
        c.Equipment.SubWeapon = IItem.FromString(data[15]);
        c.Equipment.Ring1 = IItem.FromString(data[16]);
        c.Equipment.Ring2 = IItem.FromString(data[17]);

        for (var i = 18; i < 18 + 15; i++)
        {
            c._inventory[i - 18] = IItem.FromString(data[i]);
        }

        return c;
    }
}

public static class CharacterExt
{
    public static string ToCsv(this Character c)
    {
        var resultBuilder = new StringBuilder($"{c.Name},{c.Job},{c.Level},{c.Exp}," +
                                              $"{c.LevelUpExp},{c.Hp},{c.Atk},{c.Defence}," +
                                              $"{c.Critical},{c.Speed},{c.Gold},");
        foreach (var equip in c.Equipment.EquipItems())
        {
            resultBuilder.Append($"{equip.ID}:{equip.Enhancement}:{equip.Guid},");
        }

        foreach (var item in c.Inventory)
        {
            resultBuilder.Append($"{item.ID}:{item.Enhancement}:{item.Guid},");
        }

        resultBuilder.Remove(resultBuilder.Length - 1, 1);
        return resultBuilder.ToString();
    }
}