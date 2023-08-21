using TextRpg.model;
using TextRpg.screens.inventory;

namespace TextRpg.data;

public class Repository
{
    private static Repository? _instance = null;

    public static Repository GetInstance()
    {
        return _instance ??= new Repository();
    }

    private Repository()
    {
    }

    public List<IItem> GetItems()
    {
        // todo database 또는 파일시스템 연결 이후 데이터소스에서 가져오도록 수정.
        var items = new List<IItem>(30);
        for (int row = 0; row < InventoryViewModel.Height; row++)
        {
            for (int col = 0; col < InventoryViewModel.Width; col++)
            {
                items.Add(new IItem.Empty());
            }
        }

        return items;
    }
}