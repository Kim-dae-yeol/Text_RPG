using System.Drawing;

namespace TextRpg.screens.home;

public class HomeViewModel
{
    public const int MapWidth = 30;
    public const int MapHeight = 10;
    public HomeState State { get; private set; }

    public string? Error { get; private set; } = null;

    // todo state : map, npc, interaction ...

    public SideEffect? Effect { get; private set; } = null;

    public enum SideEffect
    {
        Shop,
        Enhancement,
        Dungeon
    }

    public HomeViewModel()
    {
        var npcs = new List<Npc>();

        var shopNpc = new Npc(
            x: 5,
            y: 8,
            interactionMessage: "[ 상점 ] 물건을 구입하시려면 Enter 키를 눌러주세요.",
            npcType: Npc.NpcType.Shop);

        var enhanceNpc = new Npc(
            x: 17,
            y: 3,
            interactionMessage: "[ 강화 ] 강화에 도전하시려면 Enter 키를 눌러주세요.",
            npcType: Npc.NpcType.Enhancement);
        npcs.Add(shopNpc);
        npcs.Add(enhanceNpc);
        for (var i = 2; i <= 4; i++)
        {
            for (var j = 2; j <= 4; j++)
            {
                var dungeon = new Npc(
                    x: MapWidth - j,
                    y: MapHeight - i,
                    interactionMessage: "[ 던전 ] 던전에 입장하시려면 Enter 키를 눌러주세요.",
                    npcType: Npc.NpcType.Dungeon);
                npcs.Add(dungeon);
            }
        }


        State = new HomeState(
            map: InitMap(npcs),
            npcs: npcs,
            playerX: MapHeight / 2,
            playerY: MapHeight / 2
        );
        UpdateMap(-1, -1);
    }

    public void OnCommand(HomeScreen.CommandTypes command)
    {
        switch (command)
        {
            case HomeScreen.CommandTypes.Exit:
                break;
            case HomeScreen.CommandTypes.Inventory:
                break;
            case HomeScreen.CommandTypes.Status:
                break;
            case HomeScreen.CommandTypes.MoveUp:
                if (State.playerY > 0 &&
                    State.map[State.playerY - 1, State.playerX] == (int)Game.MapType.Space
                   )
                {
                    var prevX = State.playerX;
                    var prevY = State.playerY;
                    State = State with { playerY = State.playerY - 1 };
                    UpdateMap(prevX, prevY);
                }

                break;
            case HomeScreen.CommandTypes.MoveDown:
                if (State.playerY < MapHeight - 1 &&
                    State.map[State.playerY + 1, State.playerX] == (int)Game.MapType.Space)
                {
                    var prevX = State.playerX;
                    var prevY = State.playerY;
                    State = State with { playerY = State.playerY + 1 };
                    UpdateMap(prevX, prevY);
                }

                break;
            case HomeScreen.CommandTypes.MoveLeft:
                if (State.playerX > 0 &&
                    State.map[State.playerY, State.playerX - 1] == (int)Game.MapType.Space)
                {
                    var prevX = State.playerX;
                    var prevY = State.playerY;
                    State = State with { playerX = State.playerX - 1 };
                    UpdateMap(prevX, prevY);
                }

                break;
            case HomeScreen.CommandTypes.MoveRight:
                if (State.playerX < MapWidth - 1 &&
                    State.map[State.playerY, State.playerX + 1] == (int)Game.MapType.Space)
                {
                    var prevX = State.playerX;
                    var prevY = State.playerY;
                    State = State with { playerX = State.playerX + 1 };
                    UpdateMap(prevX, prevY);
                }

                break;
            case HomeScreen.CommandTypes.Wrong:
                Error = "잘못된 입력입니다.";
                break;
            case HomeScreen.CommandTypes.Interaction:
                if (State.CurrentInteractionNpc != null)
                {
                    InteractionWithNpc(State.CurrentInteractionNpc.type);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }
    }

    public void UpdateMap(int prevPlayerX, int prevPlayerY)
    {
        if (prevPlayerX == -1 || prevPlayerY == -1)
        {
            State.map[State.playerY, State.playerX] = (int)Game.MapType.Player;
            return;
        }

        State.map[prevPlayerY, prevPlayerX] = (int)Game.MapType.Space;
        State.map[State.playerY, State.playerX] = (int)Game.MapType.Player;
    }

    private int[,] InitMap(List<Npc> npcs)
    {
        var map = new int[MapHeight, MapWidth];
        for (var i = 0; i < MapHeight; i++)
        {
            for (var j = 0; j < MapWidth; j++)
            {
                if (i == 0 || i == MapHeight - 1)
                {
                    map[i, j] = (int)Game.MapType.WallHorizontal;
                }
                else if (j == 0 || j == MapWidth - 1)
                {
                    map[i, j] = (int)Game.MapType.WallVertical;
                }
                else
                {
                    map[i, j] = (int)Game.MapType.Space;
                }
            }
        }

        foreach (var npc in npcs)
        {
            if (npc.type == Npc.NpcType.Dungeon)
            {
                map[npc.Y, npc.X] = (int)Game.MapType.Monster;
            }
            else
            {
                map[npc.Y, npc.X] = (int)Game.MapType.Npc;
            }
        }

        return map;
    }

    public void ConsumeError()
    {
        Error = null;
    }

    private void InteractionWithNpc(Npc.NpcType type)
    {
        Effect = type switch
        {
            Npc.NpcType.Shop => SideEffect.Shop,
            Npc.NpcType.Enhancement => SideEffect.Enhancement,
            Npc.NpcType.Dungeon => SideEffect.Dungeon,
            _ => null
        };
    }
}

public class Npc
{
    public int X { get; init; }
    public int Y { get; init; }
    public string InteractionMessage { get; init; }
    public NpcType type;

    public Npc(int x, int y, string interactionMessage, NpcType npcType)
    {
        X = x;
        Y = y;
        type = npcType;
        InteractionMessage = interactionMessage;
    }

    public enum NpcType
    {
        Shop,
        Enhancement,
        Dungeon
    }
}

public record HomeState(
    int[,] map,
    int playerX,
    int playerY,
    IReadOnlyList<Npc> npcs)
{
    public Npc? CurrentInteractionNpc => npcs.FirstOrDefault(npc =>
    {
        var isXAxisAdjust = playerX - 1 == npc.X || playerX + 1 == npc.X || playerX == npc.X;
        var isYAxisAdjust = playerY - 1 == npc.Y || playerY + 1 == npc.Y || playerY == npc.Y;
        return isXAxisAdjust && isYAxisAdjust;
    });
}