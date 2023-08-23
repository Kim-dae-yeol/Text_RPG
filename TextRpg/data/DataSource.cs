using TextRpg.model;

namespace TextRpg.data;

public class DataSource
{
    private const string FileName = "SpartaCharacter.csv";
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
    private static DataSource? _instance;

    public static DataSource GetInstance()
    {
        return _instance ??= new DataSource();
    }

    private DataSource()
    {
    }

    public Character LoadFromSource()
    {
        var file = File.Open(FilePath, FileMode.OpenOrCreate);
        var reader = new StreamReader(file);
        Character c;
        using (reader)
        {
            c = Character.FromCsv(reader.ReadLine());
        }

        return c;
    }

    public void SaveToSource(Character c)
    {
        var csv = c.ToCsv();

        var stream = File.Open(FilePath, FileMode.Truncate);
        var writer = new StreamWriter(stream);
        using (writer)
        {
            writer.WriteLine(csv);
        }
    }
}