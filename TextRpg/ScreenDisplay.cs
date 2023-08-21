using System.Runtime.InteropServices.ComTypes;
using TextRpg.screens;
using TextRpg.screens.home;

namespace TextRpg;

public class ScreenDisplay
{
    private Stack<ScreenType> _backStack = new(10);
    internal IReadOnlyCollection<ScreenType> backStack => _backStack;

    internal enum ScreenType
    {
        Home,
        Status,
        Inventory
    }

    public ScreenDisplay()
    {
        _backStack.Push(ScreenType.Home);
    }

    public void DisplayCurrentScreen()
    {
        _backStack.TryPeek(out var current);
        IScreen screen = current switch
        {
            ScreenType.Home => new HomeScreen(
                marginStart: 12,
                marginTop: 3,
                navToStatusScreen: () => { _backStack.Push(ScreenType.Status); },
                navToInventory: () => { _backStack.Push(ScreenType.Inventory); },
                popBackstack: () =>
                {
                    DisplayOnExit();
                    _backStack.Pop();
                }),
            ScreenType.Status => throw new NotImplementedException(),
            ScreenType.Inventory => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        screen.StartDisplay();
    }

    private void DisplayOnExit()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("게임이 종료되었습니다.");
        Console.ResetColor();
    }
}