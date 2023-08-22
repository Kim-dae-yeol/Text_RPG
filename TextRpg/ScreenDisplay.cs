using System.Runtime.InteropServices.ComTypes;
using TextRpg.model;
using TextRpg.screens;
using TextRpg.screens.home;
using TextRpg.screens.inventory;
using TextRpg.screens.status;

namespace TextRpg;

public class ScreenDisplay
{
    private const string ArgItemType = "args_item_type";
    private Stack<ScreenType> _backStack = new(10);
    private Dictionary<string, object> _navArgs = new();
    internal IReadOnlyCollection<ScreenType> backStack => _backStack;

    internal enum ScreenType
    {
        Home,
        Status,
        Inventory,
        InventoryToEquip,
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

            ScreenType.Status => new StatusScreen(
                marginStart: 0,
                marginTop: 0,
                onBackPressed: () => { _backStack.Pop(); },
                navToInventory: (type) =>
                {
                    _navArgs[ArgItemType] = type;
                    _backStack.Push(ScreenType.InventoryToEquip);
                }),

            ScreenType.Inventory => new InventoryScreen(
                marginStart: 0,
                marginTop: 0,
                onBackPressed: () => { _backStack.Pop(); }),

            ScreenType.InventoryToEquip =>
                InventoryScreen.GetInstance(
                    marginStart: 0,
                    marginTop: 0,
                    onBackPressed: () => { _backStack.Pop(); },
                    equipType: Enum
                        .GetValues<StatusScreen.EquipmentSlotType>()
                        .First(slot => slot.ToString() == _navArgs[ArgItemType].ToString())),
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