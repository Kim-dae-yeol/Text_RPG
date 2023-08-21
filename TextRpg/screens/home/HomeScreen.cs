namespace TextRpg.screens.home;

using static TextRpg.Game;
using static System.Console;

public class HomeScreen : IScreen
{
    private int _width { get; }
    private int _height { get; }
    private int _marginStart { get; }
    private int _marginTop { get; }
    private int[,] _map;
    private Action _navToStatusScreen;
    private Action _navToInventory;
    private Action _popBackstack;

    public HomeScreen(Action navToStatusScreen,
        Action navToInventory,
        Action popBackstack,
        int marginStart = 0,
        int marginTop = 0,
        int width = 60,
        int height = 10)
    {
        _width = width;
        _height = height;
        _marginStart = marginStart;
        _marginTop = marginTop;
        _map = new int[height, width];
        _navToStatusScreen = navToStatusScreen;
        _navToInventory = navToInventory;
        _popBackstack = popBackstack;
        InitMap();
    }

    private void InitMap()
    {
        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                if (i == 0 || i == _height - 1)
                {
                    _map[i, j] = (int)MapType.WallHorizontal;
                }
                else if (j == 0 || j == _width - 1)
                {
                    _map[i, j] = (int)MapType.WallVertical;
                }
                else
                {
                    _map[i, j] = (int)MapType.Space;
                }
            }
        }

        // 맵의 뼈대를 그린다.
        // 맵에서 npc 들을 저장한다.
        // todo 맵에 있는 npc 와 상호작용을 어떻게 할것인가??
        // npc in hometown {강화, 판매}
        _map[3, 1] = (int)MapType.Npc;
        _map[3, 8] = (int)MapType.Npc;
    }

    private void DisplayHomeScreen()
    {
        Clear();
        DrawMap();
        DisplayCommands(_marginStart, _marginTop);
    }

    private void DrawMap()
    {
        // todo [Update]   2. SetCursorPosition to margin...
        SetCursorPosition(_marginStart, _marginTop);
        for (var i = 0; i < _height; i++)
        {
            SetCursorPosition(_marginStart, CursorTop);
            for (var j = 0; j < _width; j++)
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
                        Write("@");
                        ResetColor();
                        break;
                    case MapType.Player:
                        ForegroundColor = ConsoleColor.Green;
                        Write("O");
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
        // todo commandHeight to make static or constants
        var commandHeight = 7;
        for (var i = 0; i < commandHeight; i++)
        {
            WriteLine("|");
            SetCursorPosition(marginStart, CursorTop);
        }

        SetCursorPosition(marginStart + _width - 1, commandTop);
        for (var i = 0; i < commandHeight; i++)
        {
            WriteLine("|");
            SetCursorPosition(marginStart + _width - 1, CursorTop);
        }

        SetCursorPosition(marginStart, commandTop);
        for (var i = 0; i < _width; i++)
        {
            Write("-");
        }

        SetCursorPosition(marginStart, commandTop + commandHeight - 1);
        for (var i = 0; i < _width; i++)
        {
            Write("-");
        }

        SetCursorPosition(marginStart + 1, commandTop + 1);
        WriteLine("스파르타 마을에 오신 것을 환영합니다.");
        SetCursorPosition(marginStart + 1, CursorTop);
        WriteLine("마을에서는 던전으로 가기전의 활동을 할 수 있습니다.");
        SetCursorPosition(marginStart + 1, CursorTop);
        WriteLine($"{"1. 상태보기",-24} {"2. 인벤토리",24}");
        SetCursorPosition(marginStart + 1, CursorTop);
        WriteLine($"{"X. 종료하기",53}");
        SetCursorPosition(marginStart + 1, CursorTop);
    }

    private enum CommandTypes
    {
        Exit,
        Inventory,
        Status,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
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
            case CommandTypes.MoveUp:
                WriteLine("이동");
                break;
            case CommandTypes.MoveDown:
                WriteLine("이동");
                break;
            case CommandTypes.MoveLeft:
                WriteLine("이동");
                break;
            case CommandTypes.MoveRight:
                WriteLine("이동");
                break;
            case CommandTypes.Wrong:
                ForegroundColor = ConsoleColor.Red;
                Write("삐빅! 잘못된 입력입니다.");
                ResetColor();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Thread.Sleep(500);
        var isExit = command is CommandTypes.Exit or CommandTypes.Inventory or CommandTypes.Status;
        
        return !isExit;
    }

    public void DrawScreen()
    {
        DisplayHomeScreen();
    }
}