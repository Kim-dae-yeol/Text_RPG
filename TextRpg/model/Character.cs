using TextRpg.screens.status;

namespace TextRpg.model;

public class Character
{
    public string Name = "GIGA Chad";
    public Equipment Equipment = new();
    public int Level = 1;
    public int Exp = 0;
    public int LevelUpExp = 100;
    public int Hp = 100;
    public int Atk = 22;
    public float Speed = 1.0f;
    public int Critical = 0;
    public List<IItem> Inventory = new(15);


    public Dictionary<IItem.ItemEffect, int> AddedItemEffects()
    {
        var items = Equipment
            .EquipItems()
            .Where(it => it != IItem.Empty);

        var dict = new Dictionary<IItem.ItemEffect, int>();

        foreach (var item in items)
        {
            if (item.Effect1 != null)
            {
                var pair = item.Effect1.Value;
                dict.Add(pair.Key, pair.Value);
            }

            if (item.Effect2 != null)
            {
                var pair = item.Effect2.Value;
                dict.Add(pair.Key, pair.Value);
            }

            if (item.Effect3 != null)
            {
                var pair = item.Effect3.Value;
                dict.Add(pair.Key, pair.Value);
            }
        }

        return dict;
    }
}