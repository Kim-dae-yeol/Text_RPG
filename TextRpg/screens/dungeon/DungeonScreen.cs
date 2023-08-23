using static System.Console;

namespace TextRpg.screens.dungeon;

public class DungeonScreen : IScreen
{
    private DungeonViewModel _vm;
    private Action _popBackStack;

    public DungeonScreen(Action popBackStack)
    {
        _vm = new DungeonViewModel();
        _popBackStack = popBackStack;
    }

    public enum Command
    {
        Exit,
        MoveUp,
        MoveDown,
        Enter,
        Wrong
    }

    public bool ManageInput()
    {
        var key = ReadKey(true);
        var command = key.Key switch
        {
            ConsoleKey.X => Command.Exit,
            ConsoleKey.UpArrow => Command.MoveUp,
            ConsoleKey.DownArrow => Command.MoveDown,
            ConsoleKey.Enter => Command.Enter,
            _ => Command.Wrong
        };

        if (command == Command.Exit)
        {
            _popBackStack();
            return false;
        }

        _vm.OnCommand(command);
        return true;
    }

    public void DrawScreen()
    {
        if (_vm.Message != null)
        {
            ForegroundColor = _vm.MessageColor;
            SetCursorPosition(WindowWidth / 2 - _vm.Message.Length * 2, WindowHeight / 2);
            WriteLine(_vm.Message);
            ResetColor();
            _vm.ConsumeMessage();
            Thread.Sleep(800);
        }

        Clear();
        var dungeons = _vm.Dungeons;

        var idx = 0;
        var currentArrowTop = 0;
        var levelLabel = "난이도";
        var levelLabelLen = levelLabel.Length;
        for (var i = 0; i < 6 - levelLabelLen; i++)
        {
            levelLabel += "  ";
        }

        WriteLine($"    | {levelLabel,6} | {"보상",12}  | {"요구 방어력 ",6} |{"소모Hp ",6}    |");
        foreach (var dungeon in dungeons)
        {
            if (idx == _vm.CursorY)
            {
                currentArrowTop = CursorTop;
                Write(" -> ");
            }
            else
            {
                Write("    ");
            }


            WriteLine(dungeon.Desc());
            idx++;
        }

        SetCursorPosition(4, CursorTop);
        WriteLine($"{$"[Hp:{_vm.Hp}]",-20}");
        SetCursorPosition(4, CursorTop);
        WriteLine($"{$"[Defence:{_vm.Defence}]",-20}");
        WriteLine("입장하시려면 Enter, 나가시려면 x 를 눌러주세요.");
        SetCursorPosition(0, currentArrowTop);
    }
}