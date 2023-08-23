using TextRpg.data;
using TextRpg.model;

namespace TextRpg.screens.shop;

public class ShopViewModel
{
    private ShopState _state;
    private Repository _repo = Repository.GetInstance();

    public int Gold => _state.player.Gold;
    public int CurrentX => _state.cursorX;
    public int CurrentY => _state.cursorY;

    public IReadOnlyList<IItem> Inventory => _state.player.Inventory.ToList(); // copied
    public IReadOnlyList<IItem> SellingItems => _state.sellingItems;

    private string? _message = null;
    public string? Message => _message;

    private ConsoleColor _messageColor = Console.ForegroundColor;
    public ConsoleColor MessageColor => _messageColor;

    public void OnCommand(ShopScreen.Command command)
    {
        switch (command)
        {
            case ShopScreen.Command.MoveUp:
                if (CurrentY > 0)
                {
                    _state = _state with { cursorY = CurrentY - 1 };
                }

                break;
            case ShopScreen.Command.MoveDown:
                if (CurrentY < SellingItems.Count - 1)
                {
                    _state = _state with { cursorY = CurrentY + 1 };
                }

                break;
            case ShopScreen.Command.MoveRight:
                break;
            case ShopScreen.Command.MoveLeft:
                break;
            case ShopScreen.Command.Buy:
                if (_state.cursorX != 0)
                {
                    _message = "커서를 아이템창에 두고 구매해주세요.";
                    break;
                }

                BuyCurrentSelectedItem();
                break;
            case ShopScreen.Command.Wrong:
                _messageColor = ConsoleColor.Red;
                _message = "잘못된 명령입니다.";
                break;
            case ShopScreen.Command.Rest:
                Rest();
                break;
        }
    }

    public ShopViewModel()
    {
        var sellingItems = Enum
            .GetValues<IItem.ItemIds>()
            .Select(IItem.FromId)
            .Where(it => it.ID != (int)IItem.ItemIds.Empty)
            .OrderBy(it => it.Grade)
            .ToList();

        _state = new ShopState(
            cursorX: 0,
            cursorY: 0,
            sellingItems: sellingItems,
            player: _repo.player);
    }

    private void BuyCurrentSelectedItem()
    {
        var selected = SellingItems[_state.cursorY];
        var price = selected.Price();
        if (_state.player.Inventory.Count(item => item.ID == IItem.Empty.ID) == 0)
        {
            //inventory is full
            _messageColor = ConsoleColor.Blue;
            _message = "인벤토리가 가득 찼습니다.";
        }
        else if (Gold >= price)
        {
            _messageColor = ConsoleColor.Green;
            _message = "구매 성공!!";
            _state.player.Gold -= price;
            _state.player.BuyItem(SellingItems[_state.cursorY]);
            _repo.SaveData();
        }
        else
        {
            _messageColor = ConsoleColor.Blue;
            _message = "돈이 부족합니다. 던전에서 모험을 하고 구입해주세요.";
        }
    }

    public void ConsumeMessage()
    {
        _message = null;
        _messageColor = Console.ForegroundColor;
    }


    private void Rest()
    {
        if (Gold >= 500)
        {
            _messageColor = ConsoleColor.Blue;
            _message = "휴식하여서 체력을 100만큼 회복합니다!";
            _state.player.Gold -= 500;
            _state.player.Hp += 100;
        }
        else
        {
            _messageColor = ConsoleColor.Blue;
            _message = "돈이 부족합니다. 던전에서 모험을 하고 구입해주세요.";
        }
    }
}

public record ShopState(
    int cursorX,
    int cursorY,
    IReadOnlyList<IItem> sellingItems,
    Character player
);