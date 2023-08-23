namespace TextRpg.screens.home;

using static TextRpg.Game;
using static System.Console;

public class HomeScreen : IScreen
{
    private HomeViewModel _vm;
    private int _width { get; }
    private int _height { get; }
    private int _marginStart { get; }
    private int _marginTop { get; }

    private int[,] _map;

    private const int DialogWidth = 25;
    private const int DialogHeight = 3;

    private const int InteractionMessageWidth = 80;
    private const int InteractionMessageHeight = 3;
    private const int CommandHeight = 7;
    private const int CommandWidth = 80;

    private Action _navToStatusScreen;
    private Action _navToInventory;
    private Action _popBackstack;
    private Action _navToEnhancement;
    private Action _navToShop;
    private Action _navToDungeon;

    public HomeScreen(Action navToStatusScreen,
        Action navToInventory,
        Action popBackstack,
        Action navToShop,
        Action navToEnhancement,
        Action navToDungeon,
        int marginStart = 0,
        int marginTop = 0,
        int width = 60,
        int height = 10)
    {
        _width = width;
        _height = height;
        _marginStart = marginStart;
        _marginTop = marginTop;
        _vm = new HomeViewModel();
        _map = _vm.State.map;
        _navToStatusScreen = navToStatusScreen;
        _navToInventory = navToInventory;
        _popBackstack = popBackstack;
        _navToShop = navToShop;
        _navToEnhancement = navToEnhancement;
        _navToDungeon = navToDungeon;
    }


    private void DisplayHomeScreen()
    {
        Clear();
        DrawMap();
        DisplayCommands(_marginStart, _marginTop);
    }

    private void DrawMap()
    {
        var left = _marginStart + CommandWidth / 2 - HomeViewModel.MapWidth / 2;
        SetCursorPosition(left, _marginTop);
        for (var i = 0; i < HomeViewModel.MapHeight; i++)
        {
            SetCursorPosition(left, CursorTop);
            for (var j = 0; j < HomeViewModel.MapWidth; j++)
            {
                switch ((MapType)_map[i, j])
                {
                    case MapType.Space:
                        Write(" ");
                        break;
                    case MapType.WallHorizontal:
                        Write("-");
                        break;
                    case MapType.WallVertical:
                        Write("|");
                        break;
                    case MapType.Monster:
                        ForegroundColor = ConsoleColor.Red;
                        Write("u");
                        ResetColor();
                        break;
                    case MapType.Player:
                        ForegroundColor = ConsoleColor.Green;
                        Write("T");
                        ResetColor();
                        break;
                    case MapType.Npc:
                        ForegroundColor = ConsoleColor.Blue;
                        Write("@");
                        ResetColor();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            WriteLine();
        }
    }

    private void DisplayCommands(int marginStart = 0, int marginTop = 0)
    {
        var commandTop = marginTop + _height;
        SetCursorPosition(marginStart, commandTop);

        for (var i = 0; i < CommandHeight; i++)
        {
            WriteLine("|");
            SetCursorPosition(marginStart, CursorTop);
        }

        SetCursorPosition(marginStart + _width - 1, commandTop);
        for (var i = 0; i < CommandHeight; i++)
        {
            WriteLine("|");
            SetCursorPosition(marginStart + CommandWidth - 1, CursorTop);
        }

        SetCursorPosition(marginStart, commandTop);
        for (var i = 0; i < CommandWidth; i++)
        {
            Write("-");
        }

        SetCursorPosition(marginStart, commandTop + CommandHeight - 1);
        for (var i = 0; i < CommandWidth; i++)
        {
            Write("-");
        }

        SetCursorPosition(marginStart + 1, commandTop + 1);
        WriteLine("스파르타 마을에 오신 것을 환영합니다.");
        SetCursorPosition(marginStart + 1, CursorTop);
        WriteLine("마을에서는 던전으로 가기전의 활동을 할 수 있습니다.");
        SetCursorPosition(marginStart + 1, CursorTop);
        WriteLine($"{"1. 상태보기",-24} {"2. 인벤토리",45}");
        SetCursorPosition(marginStart + 1, CursorTop);
        WriteLine($"{"X. 종료하기",74}");
        SetCursorPosition(marginStart + 1, CursorTop);
    }

    public enum CommandTypes
    {
        Exit,
        Inventory,
        Status,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interaction,
        Wrong
    }


    public bool ManageInput()
    {
        var key = ReadKey(true);
        var command = key.Key switch
        {
            ConsoleKey.UpArrow => CommandTypes.MoveUp,
            ConsoleKey.DownArrow => CommandTypes.MoveDown,
            ConsoleKey.LeftArrow => CommandTypes.MoveLeft,
            ConsoleKey.RightArrow => CommandTypes.MoveRight,
            ConsoleKey.D1 => CommandTypes.Status,
            ConsoleKey.D2 => CommandTypes.Inventory,
            ConsoleKey.X => CommandTypes.Exit,
            ConsoleKey.Enter => CommandTypes.Interaction,
            _ => CommandTypes.Wrong
        };

        switch (command)
        {
            case CommandTypes.Exit:
                _popBackstack();
                break;
            case CommandTypes.Inventory:
                _navToInventory();
                break;
            case CommandTypes.Status:
                _navToStatusScreen();
                break;
            case CommandTypes.Interaction:
                InteractionWithNpc(_vm.State.CurrentInteractionNpc);
                break;
        }

        _vm.OnCommand(command);

        var isExit = command is CommandTypes.Exit
                         or CommandTypes.Inventory
                         or CommandTypes.Status
                     || command == CommandTypes.Interaction && _vm.State.CurrentInteractionNpc != null;

        return !isExit;
    }

    public void DrawScreen()
    {
        if (_vm.Effect != null)
        {
            switch (_vm.Effect)
            {
                case HomeViewModel.SideEffect.Shop:
                    _navToShop();
                    break;
                case HomeViewModel.SideEffect.Enhancement:
                    _navToEnhancement();
                    break;
                case HomeViewModel.SideEffect.Dungeon:
                    break;
            }
        }

        if (_vm.Error != null)
        {
            WrongMessage(_vm.Error);
            _vm.ConsumeError();
        }

        DisplayHomeScreen();

        if (_vm.State.CurrentInteractionNpc != null)
        {
            DisplayInteraction(_vm.State.CurrentInteractionNpc.InteractionMessage);
        }
    }

    private void WrongMessage(string msg)
    {
        var centerX = HomeViewModel.MapWidth / 2;
        var centerY = HomeViewModel.MapHeight / 2;
        var msgLen = msg.Length;

        var left = _marginStart + centerX - DialogWidth / 2;
        var top = _marginTop + centerY - DialogHeight / 2;
        SetCursorPosition(left, top);
        for (var y = 0; y < DialogHeight; y++)
        {
            for (var x = 0; x < DialogWidth; x++)
            {
                if (y == 0 || y == DialogHeight - 1)
                {
                    Write("-");
                }
                else if (x == 0 || x == DialogWidth - 1)
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

        ForegroundColor = ConsoleColor.Red;
        Beep();
        SetCursorPosition(left + msgLen / 2, top + DialogHeight / 2);
        Write(msg);
        ResetColor();
        Thread.Sleep(700);
    }

    private void DisplayInteraction(string interactionMessage)
    {
        var left = _marginStart;
        var top = _marginTop + _height + CommandHeight;
        var centerY = _marginTop + _height + CommandHeight + InteractionMessageHeight / 2;

        SetCursorPosition(left, top);
        for (var y = 0; y < InteractionMessageHeight; y++)
        {
            for (var x = 0; x < InteractionMessageWidth; x++)
            {
                if (y == 0 || y == InteractionMessageHeight - 1)
                {
                    Write("-");
                }
                else if (x == 0 || x == InteractionMessageWidth - 1)
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

        SetCursorPosition(left + 1, centerY);
        WriteLine(interactionMessage);
    }

    private void InteractionWithNpc(Npc? npc)
    {
        if (npc == null) return;

        switch (npc.type)
        {
            case Npc.NpcType.Shop:
                _navToShop();
                break;
            case Npc.NpcType.Enhancement:
                _navToEnhancement();
                break;
            case Npc.NpcType.Dungeon:
                _navToDungeon();
                break;
        }
    }
}