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

    public void EquipItemToSlot(IItem item, StatusScreen.EquipmentSlotType slot)
    {
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
                if (Equipment.Ring1.GUID == item.GUID)
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
        return Equipment.EquipItems().Contains(item);
    }
}