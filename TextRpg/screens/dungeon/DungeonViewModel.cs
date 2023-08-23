using TextRpg.data;
using TextRpg.model;

namespace TextRpg.screens.dungeon;

public class DungeonViewModel
{
    private Repository _repo = Repository.GetInstance();
    private DungeonState _state;
    public int Gold => _state.player.Gold;
    public int CursorY => _state.CursorY;
    public IReadOnlyList<IDungeon> Dungeons => _state.Dungeons;

    private string? _error = null;
    public string? Error => _error;

    public DungeonViewModel()
    {
        _state = new(0, Dungeon.Dungeons, _repo.player);
    }

    public void OnCommand(DungeonScreen.Command command)
    {
    }

    private void EnterDungeon(IDungeon dungeon)
    {
    }
}

public record DungeonState(
    int CursorY,
    IReadOnlyList<IDungeon> Dungeons,
    Character player
);