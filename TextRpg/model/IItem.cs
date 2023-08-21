namespace TextRpg.model;

public interface IItem
{
    // todo 등급추가하기
    public string Name { get; }
    public string Description { get; }
    public string Effect1 { get; }
    public string Effect2 { get; }
    public string Effect3 { get; }
    public string Type { get; }
    public string Grade { get; }
    public Func<Character> OnEquip { get; }
    public static IItem Empty = new EmptyItem();

    private class EmptyItem : IItem
    {
        public string Name => "";
        public string Description => "";
        public string Effect1 => "";
        public string Effect2 => "";
        public string Effect3 => "";
        public string Type => "";
        public string Grade => "";
        public Func<Character> OnEquip => null;
    }
}