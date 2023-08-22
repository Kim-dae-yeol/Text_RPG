namespace TextRpg.util;

public interface IDirectionTree<T>
{
    public IDirectionTree<T>? Left { get; set; }
    public IDirectionTree<T>? Right { get; set; }
    public IDirectionTree<T>? Up { get; set; }
    public IDirectionTree<T>? Down { get; set; }
    public T Current { get; }

    public void SetLeft(IDirectionTree<T> left);
    public void SetRight(IDirectionTree<T> right);
    public void SetUp(IDirectionTree<T> up);
    public void SetDown(IDirectionTree<T> down);
}