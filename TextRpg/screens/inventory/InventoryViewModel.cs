using TextRpg.data;
using TextRpg.model;
using TextRpg.model.items;
using TextRpg.screens.status;

namespace TextRpg.screens.inventory;

public class InventoryViewModel
{
    /** zero-based*/
    public const int WidthIndex = 2;

    /** zero-based*/
    public const int HeightIndex = 4;

    private Repository _repo = Repository.GetInstance();
    public InventoryState State { get; private set; }
    public string? Error { get; private set; } = null;

    public StatusScreen.EquipmentSlotType? EquipType = null;

    public InventoryViewModel()
    {
        var items = _repo.GetInventoryItems();
        var player = _repo.player;
        State = new InventoryState(0, 0, player: player, items);
    }

    public void OnCommand(InventoryScreen.Command command)
    {
        switch (command)
        {
            case InventoryScreen.Command.Equip:
                var currentSelectedType = State.CurrentSelectedItem?.Type.ToString() ?? string.Empty;
                if (State.CurrentSelectedItem != null && State.CurrentSelectedItem != IItem.Empty)
                {
                    if (EquipType == null)
                    {
                        //EquipType?.ToString()
                        //.Contains(currentSelectedType)
                        State.player.EquipItem(State.CurrentSelectedItem);
                    }
                    else if (EquipType.ToString().Contains(currentSelectedType))
                    {
                        State.player.EquipItemToSlot(State.CurrentSelectedItem, EquipType);
                    }
                    else
                    {
                        Error = "착용할 수 없는 장비입니다";
                    }
                }
                else
                {
                    Error = "비어있는 아이템은 착용할 수 없습니다";
                }

                break;
            case InventoryScreen.Command.Exit:
                _repo.SaveData();
                break;
            case InventoryScreen.Command.MoveUp:
                if (State.CurrentY > 0)
                {
                    State = State with { CurrentY = State.CurrentY - 1 };
                }

                break;
            case InventoryScreen.Command.MoveDown:
                if (State.CurrentY < HeightIndex)
                {
                    State = State with { CurrentY = State.CurrentY + 1 };
                }

                break;
            case InventoryScreen.Command.MoveLeft:
                if (State.CurrentX > 0)
                {
                    State = State with { CurrentX = State.CurrentX - 1 };
                }

                break;
            case InventoryScreen.Command.MoveRight:
                if (State.CurrentX < WidthIndex)
                {
                    State = State with { CurrentX = State.CurrentX + 1 };
                }

                break;
            case InventoryScreen.Command.Wrong:
                // do nothing
                break;
            case InventoryScreen.Command.Sort:
                SortItems();
                break;
            case InventoryScreen.Command.Throw:
                if (State.IsCurrentSelectedItemExist)
                {
                    State.player.ThrowItem(State.CurrentSelectedItem!);
                    State = State with
                    {
                        Items = _repo.GetInventoryItems()
                    };
                }
                _repo.SaveData();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }
    }

    private void SortItems()
    {
        State = State with { Items = State.Items.OrderByDescending(item => item.Name).ToList() };
    }

    public void ConsumeError()
    {
        Error = null;
    }

    public bool IsEquipped(IItem item)
    {
        return State.player.IsEquipped(item);
    }
}

public record InventoryState(
    int CurrentX,
    int CurrentY,
    Character player,
    IReadOnlyList<IItem> Items
)
{
    public IItem? CurrentSelectedItem =>
        Items.ElementAtOrDefault(CurrentY * (InventoryViewModel.WidthIndex + 1) + CurrentX);

    public bool IsCurrentSelectedItemExist => CurrentSelectedItem != null && CurrentSelectedItem != IItem.Empty;
}