using TextRpg.model;
using TextRpg.model.items;
using static System.Console;

namespace TextRpg.screens.inventory;

public class InventoryScreen : IScreen
{
    public enum Command
    {
        Equip,
        Exit,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Wrong
    }

    private const int ScreenWidth = 144;
    private const int ScreenHeight = 25;
    private const int CommandHeight = 2;
    private const int WidthPerSlot = 20;
    private const int HeightPerSlot = 4;
    private const int ItemSlotRows = 5;
    private const int ItemSlotCols = 3;
    private const int DescriptionWidth = 80;
    private const int DescMarginLeft = 0;
    private const int DescMarginTop = 2;

    private InventoryViewModel _vm;
    private int _marginStart { get; }
    private int _marginTop { get; }
    private Action _onBackPressed { get; }

    public InventoryScreen(
        int marginStart,
        int marginTop,
        Action onBackPressed)
    {
        _marginStart = marginStart;
        _marginTop = marginTop;
        _onBackPressed = onBackPressed;
        _vm = new();
    }

    public bool ManageInput()
    {
        var key = ReadKey(true);
        var command = key.Key switch
        {
            ConsoleKey.E => Command.Equip,
            ConsoleKey.X => Command.Exit,
            ConsoleKey.UpArrow => Command.MoveUp,
            ConsoleKey.DownArrow => Command.MoveDown,
            ConsoleKey.LeftArrow => Command.MoveLeft,
            ConsoleKey.RightArrow => Command.MoveRight,
            _ => Command.Wrong
        };
        if (command == Command.Wrong)
        {
            var message = "잘못된 입력입니다!";
            SetCursorPosition((ScreenWidth - message.Length) / 2, ScreenHeight + 1);
            ForegroundColor = ConsoleColor.Red;
            WriteLine(message);
            ResetColor();
            Thread.Sleep(500);
            return true;
        }

        if (command != Command.Exit)
        {
            _vm.OnCommand(command);
            return true;
        }

        _onBackPressed();
        return false;
    }

    public void DrawScreen()
    {
        Clear();
        SetCursorPosition(_marginStart, _marginTop);
        ForegroundColor = ConsoleColor.Blue;
        for (int i = 0; i < ScreenHeight; i++)
        {
            for (int j = 0; j < ScreenWidth; j++)
            {
                if (i == 0 || i == ScreenHeight - 1)
                {
                    Write("-");
                }
                else if (j == 0 || j == ScreenWidth - 1)
                {
                    Write("|");
                }
                else if (i == CommandHeight)
                {
                    Write("-");
                }
                else if (i > CommandHeight && j == DescriptionWidth)
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

        ResetColor();

        SetCursorPosition(_marginStart + 1, _marginTop + 1);
        if (_vm.State.CurrentY == -1)
        {
            ForegroundColor = ConsoleColor.Cyan;
        }

        Write(" [ X ] ");
        WriteLine($"{"E: 장비하기 ",87}");
        ResetColor();

        DisplayItemDescription();
        SetCursorPosition(_marginStart + DescMarginLeft + 1, CursorTop);

        var itemSlotStart = DescriptionWidth + 1;
        const int itemSlotTop = CommandHeight + 1;
        DisplayItemSlots(itemSlotStart + 1, itemSlotTop);
        DisplayItems(_vm.State.Items, itemSlotStart + 1, itemSlotTop);
    }

    private void DisplayItemDescription()
    {
        SetCursorPosition(_marginStart + 1 + DescMarginLeft, _marginTop + CommandHeight + 1 + DescMarginTop);

        WriteLine($" • {_vm.State.CurrentSelectedItem?.Name ?? ""}");
        SetCursorPosition(_marginStart + DescMarginLeft + 1, CursorTop);
        WriteLine($" • {_vm.State.CurrentSelectedItem?.Description ?? ""}");
        SetCursorPosition(_marginStart + DescMarginLeft + 1, CursorTop);
        WriteLine($" • {_vm.State.CurrentSelectedItem?.Effect1 ?? ""}");
        SetCursorPosition(_marginStart + DescMarginLeft + 1, CursorTop);
        WriteLine($" • {_vm.State.CurrentSelectedItem?.Effect2 ?? ""}");


        var effect3Descriptions = (_vm.State.CurrentSelectedItem?.Effect3 ?? "")
            .Split("\n")
            .Where(s => !string.IsNullOrWhiteSpace(s));

        foreach (var effect3Desc in effect3Descriptions)
        {
            SetCursorPosition(_marginStart + DescMarginLeft + 1, CursorTop);
            WriteLine($" • {effect3Desc}");
        }
    }

    private void DisplayItemSlots(int itemSlotStart, int itemSlotTop)
    {
        SetCursorPosition(itemSlotStart, itemSlotTop);

        for (var y = 0; y <= ItemSlotRows * (HeightPerSlot); y++)
        {
            for (var x = 0; x <= ItemSlotCols * (WidthPerSlot); x++)
            {
                // 4개의 숫자가 나와야함
                var minX = _vm.State.CurrentX * WidthPerSlot;
                var maxX = _vm.State.CurrentX * WidthPerSlot + WidthPerSlot;
                var minY = _vm.State.CurrentY * HeightPerSlot;
                var maxY = _vm.State.CurrentY * HeightPerSlot + HeightPerSlot;
                if (
                    x >= minX && x <= maxX &&
                    y >= minY && y <= maxY
                )
                {
                    ForegroundColor = ConsoleColor.DarkMagenta;
                }

                if (y % HeightPerSlot == 0)
                {
                    Write("-");
                }
                else if (x % WidthPerSlot == 0)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }

                ResetColor();
            }

            WriteLine();
            SetCursorPosition(itemSlotStart, CursorTop);
        }
    }

    private void DisplayItems(List<IItem> items, int itemSlotStart, int itemSlotTop)
    {
        for (var row = 0; row < ItemSlotRows; row++)
        {
            for (var col = 0; col < ItemSlotCols; col++)
            {
                var currentItem = items[row * ItemSlotCols + col];
                if (currentItem == IItem.Empty) continue;

                SetCursorPosition(
                    left: itemSlotStart + 1 + col * WidthPerSlot,
                    top: itemSlotTop + 1 + row * HeightPerSlot);
                WriteLine($"{currentItem.Name,-9}");
                SetCursorPosition(
                    left: itemSlotStart + 1 + col * WidthPerSlot,
                    top: CursorTop);
                WriteLine($"•{currentItem.Type,-9}");
                SetCursorPosition(
                    left: itemSlotStart + 1 + col * WidthPerSlot,
                    top: CursorTop);
                WriteLine($"•{currentItem.Grade,-9}");
            }
        }
    }
}