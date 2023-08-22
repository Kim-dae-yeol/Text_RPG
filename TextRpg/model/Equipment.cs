namespace TextRpg.model;

public class Equipment
{
    private IItem _helm = IItem.Empty;
    private IItem _necklace = IItem.Empty;
    private IItem _armor = IItem.Empty;
    private IItem _weapon = IItem.Empty;
    private IItem _subWeapon = IItem.Empty;
    private IItem _ring1 = IItem.Empty;
    private IItem _ring2 = IItem.Empty;

    public IItem Helm
    {
        get => _helm;
        set
        {
            if (value.Type == IItem.ItemType.Helm)
            {
                _helm = value;
            }
        }
    }

    public IItem Necklace
    {
        get => _necklace;
        set
        {
            if (value.Type == IItem.ItemType.Necklace)
            {
                _necklace = value;
            }
        }
    }

    public IItem Armor
    {
        get => _armor;
        set
        {
            if (value.Type == IItem.ItemType.Armor)
            {
                _armor = value;
            }
        }
    }

    public IItem Weapon
    {
        get => _weapon;
        set
        {
            if (value.Type == IItem.ItemType.Weapon)
            {
                _weapon = value;
            }
        }
    }

    public IItem SubWeapon
    {
        get => _subWeapon;
        set
        {
            if (value.Type == IItem.ItemType.SubWeapon)
            {
                _subWeapon = value;
            }
        }
    }

    public IItem Ring1
    {
        get => _ring1;
        set
        {
            if (value.Type == IItem.ItemType.Ring)
            {
                _ring1 = value;
            }
        }
    }

    public IItem Ring2
    {
        get => _ring2;
        set
        {
            if (value.Type == IItem.ItemType.Ring)
            {
                _ring2 = value;
            }
        }
    }

    /**helm - armor - necklace - weapon - subWeapon - ring1 - ring2**/
    public List<IItem> EquipItems()
    {
        var list = new List<IItem>();
        list.Add(Helm);
        list.Add(Armor);
        list.Add(Necklace);
        list.Add(Weapon);
        list.Add(SubWeapon);
        list.Add(Ring1);
        list.Add(Ring2);

        return list;
    }
}