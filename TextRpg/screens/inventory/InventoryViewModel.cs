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
                // todo : 인벤토리 장차크
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
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }
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
}