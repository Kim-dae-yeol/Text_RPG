namespace TextRpg.screens;

public interface IScreen
{
    public sealed void StartDisplay()
    {
        do
        {
            DrawScreen();
        } while (ManageInput());
    }

    /** return true when continue on current display, false when exit from current display*/
    private protected bool ManageInput();

    private protected void DrawScreen();
}