namespace TextRpg;

public class Game
{
    private ScreenDisplay _display = new();

    public void StartGame()
    {
        // todo 모든 클래스가 합쳐져서 여기에 온다.
        while (_display.backStack.Count > 0)
        {
            _display.DisplayCurrentScreen();
        }
    }

    public enum MapType
    {
        Space,
        WallHorizontal,
        WallVertical,
        Monster,
        Player,
        Npc
    }
}