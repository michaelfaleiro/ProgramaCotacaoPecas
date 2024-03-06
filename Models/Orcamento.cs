using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProgramaCotacaoPecas.Models;

public class Orcamento
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nome { get; set; } = null!;
    public string Carro { get; set; } = null!;

    [MaxLength(7, ErrorMessage = "Máximo 7 caracteres")]
    public string Placa { get; set; } = null!;

    [MaxLength(17, ErrorMessage = "Máximo 17 caracteres")]
    public string Chassi { get; set; } = null!;

    [BsonElement("produtos")]
    [JsonPropertyName("produtos")]
    public List<string>? Produtos { get; set; } = [];

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}