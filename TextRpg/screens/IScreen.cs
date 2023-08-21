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

    private protected bool ManageInput();

    private protected void DrawScreen();
}