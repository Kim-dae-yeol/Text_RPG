using TextRpg.model;
using TextRpg.model.items;
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

    public List<IItem> GetInventoryItems(int itemRows, int itemCols)
    {
        // todo database 또는 파일시스템 연결 이후 데이터소스에서 가져오도록 수정.
        var items = new List<IItem>(itemRows * itemCols);
        for (int row = 0; row < itemRows; row++)
        {
            for (int col = 0; col < itemCols; col++)
            {
                if (row == 3 && col == 0)
                {
                    items.Add(new Guinsoo());
                }
                else
                {
                    items.Add(IItem.Empty);
                }
            }
        }

        return items;
    }
}