namespace TextRpg.model.items;

public class Guinsoo : IItem

{
    public string Name { get; } = "구인수의 격노검";
    public string Description { get; } = "공격 속도, 방어구 관통력, 마법 관통력이 증가합니다.";
    public string Effect1 { get; } = "공격 속도 45%";
    public string Effect2 { get; } = "치명타 확률 20%";

    public string Effect3 { get; } =
        "[분노] 치명타 확률이 적중 시 의 피해량으로 전환됩니다.\n" +
        "[들끓는 일격] 세 번째 공격을 가할 때마다 적중 시 효과가 두 번 적용됩니다.\n" +
        "치명타 확률 20%당 40의 피해(적중 시 )를 입힙니다.";

    public string Type { get; } = "무기";
    public string Grade { get; } = "신화";


    public Func<Character> OnEquip { get; }
}