using TextRpg.model;
using TextRpg.model.items;

namespace TextRpg.screens.status;

using static Console;

public class StatusScreen : IScreen
{
    public enum EquipmentSlotType
    {
        Helm,
        Necklace,
        Weapon,
        Armor,
        SubWeapon,
        Ring1,
        Ring2
    }

    private const int ScreenWidth = 144;
    private const int ScreenHeight = 25;
    private const int CommandHeight = 2;
    private const int SlotsWidth = 62;
    private const int WidthPerSlot = 20;
    private const int HeightPerSlot = 4;
    private EquipmentSlotType _selectedSlot = EquipmentSlotType.Helm;
    private int _marginStart { get; }
    private int _marginTop { get; }
    private Action _onBackPressed;
    private Action<IItem.ItemType> _navToInventory;

    public StatusScreen(
        Action onBackPressed,
        Action<IItem.ItemType> navToInventory,
        int marginStart = 0,
        int marginTop = 0)
    {
        _marginStart = marginStart;
        _marginTop = marginTop;
        _onBackPressed = onBackPressed;
        _navToInventory = navToInventory;
    }

    public bool ManageInput()
    {
        var key = ReadKey(true);
        return false;
    }

    public void DrawScreen()
    {
        Clear();
        SetCursorPosition(_marginStart, _marginTop);
        for (var y = 0; y < ScreenHeight; y++)
        {
            for (var x = 0; x < ScreenWidth; x++)
            {
                if (y == 0 || y == ScreenHeight - 1)
                {
                    Write("-");
                }
                else if (x == 0 || x == ScreenWidth - 1)
                {
                    Write("|");
                }
                else if (y == CommandHeight)
                {
                    Write("-");
                }
                else if (y > CommandHeight && x == SlotsWidth)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(_marginStart, CursorTop);
        }

        SetCursorPosition(_marginStart + 1, _marginTop + 1);
        DrawCommands();

        DrawHelmSlot(
            startPos: _marginStart + WidthPerSlot + 1,
            topPos: _marginTop + CommandHeight + 1,
            helm: new Guinsoo()
        );
        // todo : Necklace Slot
        // todo : Weapon Slot
        // todo : Armor Slot
        // todo : SubWeapon Slot
        // todo : Ring1 Slot
        // todo : Ring2 Slot
        // todo : Skills Slot -> 중요도 [하]
    }

    private void DrawCommands()
    {
        Write($"{" ",10}");
        Write($"{"[ X ] 뒤로가기",20} | {"[ Enter ] 장비장착",20} | {"[ C ] 장착해제",20}");
    }

    private void DrawHelmSlot(int startPos, int topPos, IItem helm)
    {
        if (_selectedSlot == EquipmentSlotType.Helm)
        {
            ForegroundColor = ConsoleColor.DarkMagenta;
        }

        SetCursorPosition(startPos, topPos);
        for (var y = 0; y <= HeightPerSlot; y++)
        {
            for (var x = 0; x <= WidthPerSlot; x++)
            {
                if (y == 0 || y == HeightPerSlot)
                {
                    Write("-");
                }
                else if (x == 0 || x == WidthPerSlot)
                {
                    Write("|");
                }

                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(startPos, CursorTop);
        }

        ResetColor();

        SetCursorPosition(startPos + 1, topPos + 1);
        if (helm == IItem.Empty)
        {
            SetCursorPosition(CursorLeft, CursorTop + 1);
            Write("• 모자");
        }
        else
        {
            var cursorLeft = CursorLeft;
            ForegroundColor = helm.Grade.GetColor();
            WriteLine($"• {helm.Name}");
            SetCursorPosition(cursorLeft, CursorTop);
            WriteLine($"• {helm.Type.String()}");
            SetCursorPosition(cursorLeft, CursorTop);
            WriteLine($"• {helm.Grade}");
            ResetColor();
        }
    }
}