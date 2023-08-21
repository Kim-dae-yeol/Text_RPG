using TextRpg.data;
using TextRpg.model;

namespace TextRpg.screens.inventory;

public class InventoryViewModel
{
    public const int Width = 3;
    public const int Height = 5;
    private Repository _repo = Repository.GetInstance();
    public InventoryState State { get; private set; }

    public InventoryViewModel()
    {
        var items = _repo.GetItems();
        State = new InventoryState(0, 0, items);
    }

    public void ChangeCurrentInventorySlot(int x, int y)
    {
        // todo 인벤토리의 슬롯 변경시 호출되는 함수이다.
    }
}

public record InventoryState(
    int CurrentX,
    int CurrentY,
    List<IItem> Items
)
{
    public IItem CurrentSelectedItem => Items[CurrentY * InventoryViewModel.Height + CurrentX];
}