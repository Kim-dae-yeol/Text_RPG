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


    private const int ScreenWidth = 124;
    private const int ScreenHeight = 24;
    private const int CommandHeight = 2;
    private const int SlotsWidth = 64;
    private const int WidthPerSlot = 20;
    private const int HeightPerSlot = 4;

    private StatusViewModel _vm = new();
    private EquipmentSlotType _selectedSlot => _vm.CurrentSelected;
    private Equipment _equipment => _vm.Equipment;
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
        Beep();
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

        _vm.OnCommand(command);
        switch (command)
        {
            case Command.Exit:
                _onBackPressed();
                return false;
            case Command.Equip:
                _navToInventory(_selectedSlot);
                return false;
            case Command.UnEquip:
                return true;
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
        DrawCharacterInformation();
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
            item: _equipment.Helm
        );

        DrawSlot(
            startPos: _marginStart + WidthPerSlot * 2 + 3,
            topPos: _marginTop + HeightPerSlot + CommandHeight + 2,
            slotType: EquipmentSlotType.Necklace,
            item: _equipment.Necklace
        );

        DrawSlot(
            startPos: _marginStart + 1,
            topPos: _marginTop + HeightPerSlot * 2 + CommandHeight + 3,
            slotType: EquipmentSlotType.Weapon,
            item: _equipment.Weapon
        );
        DrawSlot(
            startPos: _marginStart + WidthPerSlot + 2,
            topPos: _marginTop + HeightPerSlot * 2 + CommandHeight + 3,
            slotType: EquipmentSlotType.Armor,
            item: _equipment.Armor
        );
        DrawSlot(
            startPos: _marginStart + WidthPerSlot * 2 + 3,
            topPos: _marginTop + HeightPerSlot * 2 + CommandHeight + 3,
            slotType: EquipmentSlotType.SubWeapon,
            item: _equipment.SubWeapon
        );
        DrawSlot(
            startPos: _marginStart + 1,
            topPos: _marginTop + HeightPerSlot * 3 + CommandHeight + 4,
            slotType: EquipmentSlotType.Ring1,
            item: _equipment.Ring1
        );
        DrawSlot(
            startPos: _marginStart + WidthPerSlot * 2 + 3,
            topPos: _marginTop + HeightPerSlot * 3 + CommandHeight + 4,
            slotType: EquipmentSlotType.Ring2,
            item: _equipment.Ring2
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

    private void DrawCharacterInformation()
    {
        var left = _marginStart + SlotsWidth + 10 + 1;
        var top = CommandHeight + 1 + 5;
        var appliedEffects = _vm.AddedItemEffects();
        var c = _vm.ReadOnlyPlayerInfo();
        SetCursorPosition(left, top);

        Write($"{" • Name",-20}");
        WriteLine($"| {c.Name} ( {c.Job} )");
        SetCursorPosition(left, CursorTop);

        Write($"{" • Lv",-20}");
        WriteLine($"| {c.Level}");
        SetCursorPosition(left, CursorTop);

        Write($"{" • Exp",-20}");
        WriteLine($"| {c.Exp} / {c.LevelUpExp}");
        SetCursorPosition(left, CursorTop);

        Write($"{" • Hp ",-20}");
        Write($"| {c.Hp}");

        //todo color to method
        var addedHp = appliedEffects.GetValueOrDefault(IItem.ItemEffect.Hp, 0);
        if (addedHp != 0)
        {
            ForegroundColor = ConsoleColor.Yellow;
            Write($"( + {addedHp} )");
            ResetColor();
        }

        WriteLine();
        SetCursorPosition(left, CursorTop);

        Write($"{" • Atk ",-20}");
        Write($"| {c.Atk}");
        var addedAtk = appliedEffects.GetValueOrDefault(IItem.ItemEffect.Atk, 0);
        if (addedAtk != 0)
        {
            ForegroundColor = ConsoleColor.Yellow;
            Write($"( + {addedAtk} )");
            ResetColor();
        }

        WriteLine();
        SetCursorPosition(left, CursorTop);
        Write($"{" • Defence ",-20}");
        Write($"| {c.Defence}");

        //todo color to method
        var addedDef = appliedEffects.GetValueOrDefault(IItem.ItemEffect.Defence, 0);
        if (addedDef != 0)
        {
            ForegroundColor = ConsoleColor.Yellow;
            Write($"( + {addedDef} )");
            ResetColor();
        }

        WriteLine();
        SetCursorPosition(left, CursorTop);
        Write($"{" • Speed ",-20}");
        Write($"| {c.Speed:N2}");

        var addedSpd = appliedEffects.GetValueOrDefault(IItem.ItemEffect.Speed, 0);
        if (addedSpd != 0)
        {
            ForegroundColor = ConsoleColor.Yellow;
            Write($"( + {addedSpd} )");
            ResetColor();
        }

        WriteLine();
        SetCursorPosition(left, CursorTop);

        Write($"{" • Critical ",-20}");
        Write($"| {c.Critical}");

        var addedCri = appliedEffects.GetValueOrDefault(IItem.ItemEffect.Critical, 0);
        if (addedCri != 0)
        {
            ForegroundColor = ConsoleColor.Yellow;
            Write($"( + {addedCri} )");
            ResetColor();
        }

        SetCursorPosition(left, CursorTop);

        Write($"{" • Gold ",-20}");
        WriteLine($"| {c.Gold}");
        SetCursorPosition(left, CursorTop);
    }
}