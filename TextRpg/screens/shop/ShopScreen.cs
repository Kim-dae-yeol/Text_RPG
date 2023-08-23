using static System.Console;

namespace TextRpg.screens.shop;

public class ShopScreen : IScreen
{
    private ShopViewModel _vm = new();
    private Action _popBackStack;

    public ShopScreen(Action popBackStack)
    {
        _popBackStack = popBackStack;
    }

    public enum Command
    {
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        Buy,
        Exit,
        Rest,
        Wrong
    }

    public bool ManageInput()
    {
        var key = ReadKey(true);
        var command = key.Key switch
        {
            ConsoleKey.UpArrow => Command.MoveUp,
            ConsoleKey.DownArrow => Command.MoveDown,
            ConsoleKey.LeftArrow => Command.MoveLeft,
            ConsoleKey.RightArrow => Command.MoveRight,
            ConsoleKey.Enter => Command.Buy,
            ConsoleKey.X => Command.Exit,
            ConsoleKey.R => Command.Rest,
            _ => Command.Wrong
        };

        if (command == Command.Exit)
        {
            _popBackStack();
        }

        _vm.OnCommand(command);
        return command != Command.Exit;
    }

    public void DrawScreen()
    {
        if (_vm.Message != null)
        {
            OnMessage(_vm.Message);
            Thread.Sleep(700);
        }

        Clear();
        WriteLine("상점입니다 !!!  구매하시려면 Enter, 500원을 내고 휴식하시려면 R, 나가시려면 x 키를 눌러주세요.");
        for (var i = 0; i < _vm.SellingItems.Count; i++)
        {
            if (_vm.CurrentY == i && _vm.CurrentX == 0)
            {
                ForegroundColor = ConsoleColor.Cyan;
                Write(" -> ");
                ResetColor();
            }
            else
            {
                Write("    ");
            }

            var item = _vm.SellingItems[i];
            ForegroundColor = item.Grade.GetColor();
            var nameString = item.Name.Replace(" ", "");
            float nameStringLen = nameString.Length;

            for (var j = 0; j < nameString.Length; j++)
            {
                if (nameString[j] >= 'a' && nameString[j] <= 'z' ||
                    nameString[j] >= 'A' && nameString[j] <= 'Z')
                {
                    nameStringLen -= 0.5f;
                }
            }


            var blankToName = "";
            for (var j = 0; j < (10 - nameStringLen) * 2; j++)
            {
                blankToName += " ";
            }

            var descString = item.Description;
            var descLen = descString.Length;
            var notUniInDesc = 0;

            for (var j = 0; j < descString.Length; j++)
            {
                if (descString[j] >= 'a' && descString[j] <= 'z' ||
                    descString[j] >= 'A' && descString[j] <= 'Z' ||
                    descString[j] == ' ' ||
                    descString[j] == ',' ||
                    descString[j] == '.')
                {
                    notUniInDesc++;
                }
            }

            var blankToDesc = "";
            for (var j = 0; j < (40 - descLen) * 2 + notUniInDesc; j++)
            {
                blankToDesc += " ";
            }

            WriteLine($"[ {i + 1,3} ] : {nameString + blankToName,-10} |" +
                      $" {item.Description + blankToDesc,-40} |" +
                      $" {item.Grade,15} | " +
                      $"{item.Price(),20:C} |");
            ResetColor();
        }

        SetCursorPosition(WindowWidth - 20, CursorTop);
        WriteLine($"{$"[ Gold : {_vm.Gold}]",20}");
        SetCursorPosition(_vm.CurrentX, _vm.CurrentY + 1);
    }

    private void OnMessage(string msg)
    {
        var lenOfMessage = msg.Replace(" ", "").Length * 2;
        lenOfMessage += msg.Count(it => it == ' ');
        var messageHeight = 5;
        var messageWidth = 60;
        var windowLeft = WindowWidth / 2 - messageWidth / 2;
        var windowTop = WindowHeight / 2 - messageHeight / 2;
        var messageLeft = WindowWidth / 2 - lenOfMessage / 2;
        var messageTop = windowTop + messageHeight / 2;

        SetCursorPosition(windowLeft, windowTop);
        for (var y = 0; y < messageHeight; y++)
        {
            for (var x = 0; x < messageWidth; x++)
            {
                if (y == 0 || y == messageHeight - 1)
                {
                    Write("-");
                }
                else if (x == 0 || x == messageWidth - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(windowLeft, CursorTop);
        }

        ForegroundColor = _vm.MessageColor;
        SetCursorPosition(messageLeft, messageTop);
        WriteLine(msg);
        ResetColor();
        _vm.ConsumeMessage();
    }
}