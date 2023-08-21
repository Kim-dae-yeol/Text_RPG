namespace TextRpg;

public class Game
{
    private ScreenDisplay _display = new();

    public void StartGame()
    {
        // todo 모든 클래스가 합쳐져서 여기에 온다.
        /*  todo 작성할 클래스들 목록
            1. 아이템 모델링  
            2. 인벤토리 모델클래스 
            3. 던전 클래스 
            4. 플레이어 모델링
            5. 상태화면 클래스 
            6. 인벤토리화면 클래스
         */ 
        do
        {
            _display.DisplayCurrentScreen();
        } while (_display.backStack.Count > 0);
        // todo : 게임을 여기서 저장함.
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