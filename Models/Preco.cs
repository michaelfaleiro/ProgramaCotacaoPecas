using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProgramaCotacaoPecas.Models;

public class Preco
{
    public Preco()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public string? Id { get; set; }

    public string Fornecedor { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public double PrecoCusto { get; set; }
    public int QuantidadeAtendida { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}