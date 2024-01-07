using Newtonsoft.Json;

namespace Lab4.repositories;

public class JsonRepository<T>
{
    public void Save(T data, string filePath)
    {
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented};
        var serializedData = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(filePath, serializedData);
    }

    public T Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else throw new ArgumentException($"File {filePath} does not exist!");
    }
}