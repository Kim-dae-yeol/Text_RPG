namespace TextRpg.screens.inventory;

public class InventoryScreen : IScreen
{
    private InventoryViewModel _vm;
    private int _width { get; }
    private int _height { get; }
    private int _marginStart { get; }
    private int _marginTop { get; }
    private Action _onBackPressed { get; }

    public InventoryScreen(
        int width,
        int height,
        int marginStart,
        int marginTop,
        Action onBackPressed)
    {
        _width = width;
        _height = height;
        _marginStart = marginStart;
        _marginTop = marginTop;
        _onBackPressed = onBackPressed;
        _vm = new();
    }

    public bool ManageInput()
    {
        return true;
    }

    public void DrawScreen()
    {
    }
}