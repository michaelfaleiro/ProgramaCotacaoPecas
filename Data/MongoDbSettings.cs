namespace ProgramaCotacaoPecas.Data;

public class MongoDbSettings
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public Dictionary<string, string> CollectionName { get; set; } = [];
}