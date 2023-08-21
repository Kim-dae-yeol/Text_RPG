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
        var current = _backStack.Peek();

        switch (current)
        {
            case ScreenType.Home:
                var homeScreen = new HomeScreen();
                homeScreen.DisplayHomeScreen(
                    navToStatusScreen: () => { },
                    navToInventory: () => { },
                    popBackstack: () => { _backStack.Pop(); });
                break;

            default:
                throw new NotImplementedException();
        }
    }
}