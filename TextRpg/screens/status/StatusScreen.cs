using TextRpg.model;
using TextRpg.model.items;

namespace TextRpg.screens.status;

using static Console;

public class StatusScreen : IScreen
{
    public enum Command
    {
        Exit,
        Equip,
        UnEquip,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Wrong
    }

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

    //todo 이 변수 viewModel 로 빼기 
    private EquipmentSlotType _selectedSlot = EquipmentSlotType.Helm;

    private int _marginStart { get; }
    private int _marginTop { get; }
    private Action _onBackPressed;
    private Action<EquipmentSlotType> _navToInventory;

    public StatusScreen(
        Action onBackPressed,
        Action<EquipmentSlotType> navToInventory,
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
        var command = key.Key switch
        {
            ConsoleKey.X => Command.Exit,
            ConsoleKey.Enter => Command.Equip,
            ConsoleKey.C => Command.UnEquip,
            ConsoleKey.UpArrow => Command.MoveUp,
            ConsoleKey.DownArrow => Command.MoveDown,
            ConsoleKey.LeftArrow => Command.MoveLeft,
            ConsoleKey.RightArrow => Command.MoveRight,
            _ => Command.Wrong
        };

        switch (command)
        {
            case Command.Exit:
                _onBackPressed();
                return false;
            case Command.Equip:
                _navToInventory(_selectedSlot);
                return false;
            case Command.UnEquip: throw new NotImplementedException();
            case Command.Wrong:
                WrongMessage();
                return true;
            case Command.MoveUp:
                return true;
            case Command.MoveDown:
                return true;
            case Command.MoveLeft:
                return true;
            case Command.MoveRight:
                return true;
            default:
                return true;
        }
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

        DrawSlots();
    }

    private void DrawCommands()
    {
        Write($"{" ",10}");
        Write($"{"[ X ] 뒤로가기",20} | {"[ Enter ] 장비장착",20} | {"[ C ] 장착해제",20}");
    }

    private void DrawSlot(
        int startPos,
        int topPos,
        EquipmentSlotType slotType,
        IItem item)
    {
        if (_selectedSlot == slotType)
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
        if (item == IItem.Empty)
        {
            SetCursorPosition(CursorLeft, CursorTop + 1);
            Write($"• {SlotTypeString(slotType)}");
        }
        else
        {
            var cursorLeft = CursorLeft;
            ForegroundColor = item.Grade.GetColor();
            WriteLine($"• {item.Name}");
            SetCursorPosition(cursorLeft, CursorTop);
            WriteLine($"• {item.Type.String()}");
            SetCursorPosition(cursorLeft, CursorTop);
            WriteLine($"• {item.Grade}");
            ResetColor();
        }
    }

    private void DrawSlots()
    {
        // todo : indexX, indexY 를 지정해서 그걸 이용해서 WidthPerSlot 을 곱해서 해당 함수 호출
        DrawSlot(
            startPos: _marginStart + WidthPerSlot + 1,
            topPos: _marginTop + CommandHeight + 1,
            slotType: EquipmentSlotType.Helm,
            item: IItem.Empty
        );

        DrawSlot(
            startPos: _marginStart + WidthPerSlot * 2 + 1,
            topPos: _marginTop + HeightPerSlot + CommandHeight + 1,
            slotType: EquipmentSlotType.Necklace,
            item: IItem.Empty
        );

        DrawSlot(
            startPos: _marginStart + 1,
            topPos: _marginTop + HeightPerSlot * 2 + CommandHeight + 1,
            slotType: EquipmentSlotType.Weapon,
            item: new Guinsoo()
        );
        DrawSlot(
            startPos: _marginStart + WidthPerSlot + 1,
            topPos: _marginTop + HeightPerSlot * 2 + CommandHeight + 1,
            slotType: EquipmentSlotType.Armor,
            item: IItem.Empty
        );
        DrawSlot(
            startPos: _marginStart + WidthPerSlot * 2 + 1,
            topPos: _marginTop + HeightPerSlot * 2 + CommandHeight + 1,
            slotType: EquipmentSlotType.SubWeapon,
            item: IItem.Empty
        );
        DrawSlot(
            startPos: _marginStart + 1,
            topPos: _marginTop + HeightPerSlot * 3 + CommandHeight + 1,
            slotType: EquipmentSlotType.Ring1,
            item: IItem.Empty
        );
        DrawSlot(
            startPos: _marginStart + WidthPerSlot * 2 + 1,
            topPos: _marginTop + HeightPerSlot * 3 + CommandHeight + 1,
            slotType: EquipmentSlotType.Ring2,
            item: IItem.Empty
        );
        // todo : Skills Slot -> 중요도 [하]
    }

    private string SlotTypeString(EquipmentSlotType slotType)
    {
        return slotType switch
        {
            EquipmentSlotType.Helm => "모자",
            EquipmentSlotType.Necklace => "목걸이",
            EquipmentSlotType.Weapon => "무기",
            EquipmentSlotType.Armor => "갑옷",
            EquipmentSlotType.SubWeapon => "보조무기",
            EquipmentSlotType.Ring1 => "반지 1",
            EquipmentSlotType.Ring2 => "반지 2",
            _ => throw new ArgumentOutOfRangeException(nameof(slotType), slotType, null)
        };
    }

    private void WrongMessage()
    {
        var dialogWidth = 30;
        var dialogHeight = 3;
        var centerX = ScreenWidth / 2;
        var centerY = ScreenHeight / 2;

        var left = centerX - dialogWidth / 2;
        var top = centerY - dialogHeight / 2;

        SetCursorPosition(left, top);
        for (var y = 0; y < dialogHeight; y++)
        {
            for (var x = 0; x < dialogWidth; x++)
            {
                if (y == 0 || y == dialogHeight - 1)
                {
                    Write("-");
                }
                else if (x == 0 || x == dialogWidth - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(left, CursorTop);
        }

        var msg = "잘못된 입력입니다.";
        ForegroundColor = ConsoleColor.Red;
        SetCursorPosition(centerX - msg.Length, centerY);
        WriteLine(msg);
        ResetColor();
        Thread.Sleep(1500);
    }
}