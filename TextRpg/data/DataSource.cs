using TextRpg.model;

namespace TextRpg.data;

public class DataSource
{
    public Character LoadFromSource()
    {
        //todo read csv file parse to character
        return new Character();
    }

    public void SaveToSource(Character c)
    {
        //todo save character to csv file
    }
}