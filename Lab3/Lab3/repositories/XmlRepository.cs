using System.Xml.Serialization;

namespace Lab3.repositories;

public class XmlRepository<T> where T : class
{
    public void Save(T data, string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var textWriter = new StreamWriter(filePath, false);
        serializer.Serialize(textWriter, data);
    }

    public T? Load(string filePath)
    {
        if (!File.Exists(filePath)) throw new Exception($"File {filePath} doesn't exist!");
        var serializer = new XmlSerializer(typeof(T));
        using var textReader = new StreamReader(filePath);
        return serializer.Deserialize(textReader) as T;
    }
}