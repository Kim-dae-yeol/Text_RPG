using TextRpg.model;
using TextRpg.model.items;
using TextRpg.screens.inventory;

namespace TextRpg.data;

public class Repository
{
    public Character player { get; private set; }
    private readonly DataSource _dataSource = DataSource.GetInstance();

    private static Repository? _instance;

    public static Repository GetInstance()
    {
        return _instance ??= new Repository();
    }

    private Repository()
    {
        player = _dataSource.LoadFromSource();
    }

    public IReadOnlyList<IItem> GetInventoryItems()
    {
        return player.Inventory;
    }

    public void SaveData()
    {
        _dataSource.SaveToSource(player);
    }
}