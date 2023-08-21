namespace TextRpg.model;

public interface IItem
{
    public string Name { get; }
    public string Description { get; }
    public string Effect1 { get; }
    public string Effect2 { get; }
    public string Effect3 { get; }
    public Func<Character> OnEquip { get; }

    public class Empty : IItem
    {
        public string Name => "";
        public string Description => "";
        public string Effect1 => "";
        public string Effect2 => "";
        public string Effect3 => "";
        public Func<Character> OnEquip => null;
    }
}