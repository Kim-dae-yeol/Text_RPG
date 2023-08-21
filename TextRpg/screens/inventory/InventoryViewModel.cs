using TextRpg.data;
using TextRpg.model;
using TextRpg.model.items;

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

    public InventoryViewModel()
    {
        var items = _repo.GetInventoryItems(HeightIndex + 1, WidthIndex + 1);
        State = new InventoryState(0, 0, items);
    }

    public void OnCommand(InventoryScreen.Command command)
    {
        switch (command)
        {
            case InventoryScreen.Command.Equip:
                // todo : 인벤토리 장착 하려면 상태창 만들고 하기 !!
                break;
            case InventoryScreen.Command.Exit:
                // do nothing
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
                    var deletedAtSelection = State.Items;
                    deletedAtSelection[State.CurrentY * (WidthIndex + 1) + State.CurrentX] = IItem.Empty;

                    State = State with
                    {
                        Items = deletedAtSelection
                    };
                }

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
}

public record InventoryState(
    int CurrentX,
    int CurrentY,
    List<IItem> Items
)
{
    public IItem? CurrentSelectedItem =>
        Items.ElementAtOrDefault(CurrentY * (InventoryViewModel.WidthIndex + 1) + CurrentX);

    public bool IsCurrentSelectedItemExist => CurrentSelectedItem != null && CurrentSelectedItem != IItem.Empty;
}