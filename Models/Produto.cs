using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProgramaCotacaoPecas.Models;

namespace ProgramaCotacaoPecas.Services;

public class Produto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string NomeProduto { get; set; } = null!;
    public int Quantidade { get; set; }

    [BsonElement("precos")]
    [JsonPropertyName("precos")]
    public List<Preco>? Precos { get; set; } = [];

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}