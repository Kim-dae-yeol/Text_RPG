using TextRpg.data;
using TextRpg.model;

namespace TextRpg.screens.dungeon;

public class DungeonViewModel
{
    private Repository _repo = Repository.GetInstance();
    private DungeonState _state;
    public int Gold => _state.player.Gold;
    public int Defence => _state.player.Defence;
    public int Hp => _state.player.Hp;
    public int CursorY => _state.CursorY;
    public IReadOnlyList<IDungeon> Dungeons => _state.Dungeons;

    private string? _message = null;
    public string? Message => _message;
    private ConsoleColor _messageColor = Console.ForegroundColor;
    public ConsoleColor MessageColor => _messageColor;

    public DungeonViewModel()
    {
        _state = new(0, Dungeon.Dungeons, _repo.player);
    }

    public void OnCommand(DungeonScreen.Command command)
    {
        switch (command)
        {
            case DungeonScreen.Command.MoveUp:
                if (CursorY > 0)
                {
                    _state = _state with { CursorY = CursorY - 1 };
                }

                break;
            case DungeonScreen.Command.MoveDown:
                if (CursorY < Dungeons.Count - 1)
                {
                    _state = _state with { CursorY = CursorY + 1 };
                }

                break;
            case DungeonScreen.Command.Enter:
                EnterDungeon(Dungeons[_state.CursorY]);
                _repo.SaveData();
                break;
            case DungeonScreen.Command.Wrong:
                _message = "잘못된 입력입니다";
                break;
        }
    }

    private void EnterDungeon(IDungeon dungeon)
    {
        if (dungeon.RequiresHp >= _state.player.Hp)
        {
            _messageColor = ConsoleColor.Red;
            _message = "체력이 부족합니다.";
            return;
        }

        if (dungeon.IsClear(_state.player))
        {
            //clear
            _state.player.Hp -= dungeon.RequiresHp;
            _state.player.Gold += dungeon.RewardGold;
            _messageColor = ConsoleColor.Blue;
            _message = $"던전을 클리어했습니다.[Gold + {dungeon.RewardGold}]";
        }
        else
        {
            _messageColor = ConsoleColor.Blue;
            _message = "던전을 클리어하는데 실패했습니다.";
        }
    }

    public void ConsumeMessage()
    {
        _message = null;
        _messageColor = Console.ForegroundColor;
    }
}

public record DungeonState(
    int CursorY,
    IReadOnlyList<IDungeon> Dungeons,
    Character player
);