using TextRpg.data;
using TextRpg.model;
using TextRpg.util;

namespace TextRpg.screens.status;

public class StatusViewModel
{
    private Repository _repo = Repository.GetInstance();

    private StatusState State { get; set; }
    public StatusScreen.EquipmentSlotType CurrentSelected => State.CurrentSelected;
    public Equipment Equipment => State.Player.Equipment;

    public StatusViewModel()
    {
        State = new StatusState(
            Player: _repo.player,
            Tree: InitTree());
    }

    public void OnCommand(StatusScreen.Command command)
    {
        switch (command)
        {
            case StatusScreen.Command.UnEquip:
                UnEquip(CurrentSelected);
                break;
            case StatusScreen.Command.MoveUp:
                if (State.Tree.Up != null)
                {
                    State = State with { Tree = State.Tree.Up };
                }

                break;
            case StatusScreen.Command.MoveDown:
                if (State.Tree.Down != null)
                {
                    State = State with { Tree = State.Tree.Down };
                }

                break;
            case StatusScreen.Command.MoveLeft:
                if (State.Tree.Left != null)
                {
                    State = State with { Tree = State.Tree.Left };
                }

                break;
            case StatusScreen.Command.MoveRight:
                if (State.Tree.Right != null)
                {
                    State = State with { Tree = State.Tree.Right };
                }

                break;
            case StatusScreen.Command.Exit:
                _repo.SaveData();
                break;
            default:
                //do nothing
                break;
        }
    }

    private IDirectionTree<StatusScreen.EquipmentSlotType> InitTree()
    {
        var ring2 = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.Ring2);
        var ring1 = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.Ring1);

        ring2.Left = ring1;
        ring1.Right = ring2;

        var weapon = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.Weapon);
        var armor = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.Armor);
        var subWeapon = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.SubWeapon);
        var helm = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.Helm);
        var necklace = new SlotDirectionTree(current: StatusScreen.EquipmentSlotType.Necklace);

        ring1.Up = weapon;
        ring2.Up = subWeapon;

        weapon.Down = ring1;
        weapon.Right = armor;
        weapon.Up = helm;

        armor.Down = ring1;
        armor.Left = weapon;
        armor.Right = subWeapon;
        armor.Up = helm;

        subWeapon.Down = ring2;
        subWeapon.Left = armor;
        subWeapon.Up = necklace;

        necklace.Down = subWeapon;
        necklace.Left = armor;
        necklace.Up = helm;

        helm.Right = necklace;
        helm.Left = weapon;
        helm.Down = armor;
        return helm;
    }

    public Dictionary<IItem.ItemEffect, int> AddedItemEffects()
    {
        return State.Player.AddedItemEffects();
    }

    public Character ReadOnlyPlayerInfo()
    {
        //todo copy this object 
        return State.Player;
    }

    private void UnEquip(StatusScreen.EquipmentSlotType type)
    {
        switch (type)
        {
            case StatusScreen.EquipmentSlotType.Helm:
                _repo.player.UnEquipItem(State.Player.Equipment.Helm);
                break;
            case StatusScreen.EquipmentSlotType.Necklace:
                _repo.player.UnEquipItem(State.Player.Equipment.Necklace);
                break;
            case StatusScreen.EquipmentSlotType.Weapon:
                _repo.player.UnEquipItem(State.Player.Equipment.Weapon);
                break;
            case StatusScreen.EquipmentSlotType.Armor:
                _repo.player.UnEquipItem(State.Player.Equipment.Armor);
                break;
            case StatusScreen.EquipmentSlotType.SubWeapon:
                _repo.player.UnEquipItem(State.Player.Equipment.SubWeapon);
                break;
            case StatusScreen.EquipmentSlotType.Ring1:
                _repo.player.UnEquipItem(State.Player.Equipment.Ring1);
                break;
            case StatusScreen.EquipmentSlotType.Ring2:
                _repo.player.UnEquipItem(State.Player.Equipment.Ring2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        State = State with { Player = _repo.player };
    }
}

public record StatusState(
    Character Player,
    IDirectionTree<StatusScreen.EquipmentSlotType> Tree)
{
    public StatusScreen.EquipmentSlotType CurrentSelected => Tree.Current;
}

public class SlotDirectionTree : IDirectionTree<StatusScreen.EquipmentSlotType>
{
    public IDirectionTree<StatusScreen.EquipmentSlotType>? Left { get; set; }
    public IDirectionTree<StatusScreen.EquipmentSlotType>? Right { get; set; }
    public IDirectionTree<StatusScreen.EquipmentSlotType>? Up { get; set; }
    public IDirectionTree<StatusScreen.EquipmentSlotType>? Down { get; set; }
    public StatusScreen.EquipmentSlotType Current { get; }

    public void SetLeft(IDirectionTree<StatusScreen.EquipmentSlotType> left)
    {
        Left = left;
    }

    public void SetRight(IDirectionTree<StatusScreen.EquipmentSlotType> right)
    {
        Right = right;
    }

    public void SetUp(IDirectionTree<StatusScreen.EquipmentSlotType> up)
    {
        Up = up;
    }

    public void SetDown(IDirectionTree<StatusScreen.EquipmentSlotType> down)
    {
        Down = down;
    }

    public SlotDirectionTree(
        StatusScreen.EquipmentSlotType current,
        IDirectionTree<StatusScreen.EquipmentSlotType>? left = null,
        IDirectionTree<StatusScreen.EquipmentSlotType>? right = null,
        IDirectionTree<StatusScreen.EquipmentSlotType>? up = null,
        IDirectionTree<StatusScreen.EquipmentSlotType>? down = null
    )
    {
        Current = current;
        Left = left;
        Right = right;
        Up = up;
        Down = down;
    }
}