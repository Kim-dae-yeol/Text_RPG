using TextRpg.model;

namespace TextRpg;

public static class Extensions
{
    internal static string String(this IItem.ItemType type)
    {
        return type switch
        {
            IItem.ItemType.Weapon => "무기",
            IItem.ItemType.Helm => "모자",
            IItem.ItemType.SubWeapon => "보조무기",
            IItem.ItemType.Armor => "갑옷",
            IItem.ItemType.Ring => "반지",
            IItem.ItemType.Necklace => "목걸이",
            IItem.ItemType.Nothing => "",
            _ => ""
        };
    }
    internal static ConsoleColor GetColor(this IItem.ItemGrade grade)
    {
        return grade switch
        {
            IItem.ItemGrade.Normal => ConsoleColor.White,
            IItem.ItemGrade.Rare => ConsoleColor.Blue,
            IItem.ItemGrade.Epic => ConsoleColor.Magenta,
            IItem.ItemGrade.Legendary => ConsoleColor.Red,
            IItem.ItemGrade.Mythic => ConsoleColor.DarkYellow,
            _ => throw new ArgumentOutOfRangeException(nameof(grade), grade, null)
        };
    }

    internal static string String(this IItem.ItemGrade grade)
    {
        return grade switch
        {
            IItem.ItemGrade.Normal =>"노멀",
            IItem.ItemGrade.Rare => "레어",
            IItem.ItemGrade.Epic => "에픽",
            IItem.ItemGrade.Legendary => "전설",
            IItem.ItemGrade.Mythic => "신화",
            _ => throw new ArgumentOutOfRangeException(nameof(grade), grade, null)
        };
    }
}