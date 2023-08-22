using TextRpg.model;
using TextRpg.model.items;
using TextRpg.screens.inventory;

namespace TextRpg.data;

public class Repository
{
    public Character player { get; private set; } = new Character();
    private static Repository? _instance = null;

    public static Repository GetInstance()
    {
        return _instance ??= new Repository();
    }

    private Repository()
    {
    }

    public IReadOnlyList<IItem> GetInventoryItems()
    {
        return player.Inventory;
    }
}