using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProgramaCotacaoPecas.Models;
using ProgramaCotacaoPecas.Services;

namespace ProgramaCotacaoPecas.ViewsModels;

public class ListaProdutoViewModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Carro { get; set; }
    public string Placa { get; set; }
    public string Chassi { get; set; }
    public string Observacao { get; set; }
    public string Status { get; set; }

    [BsonElement("Produtos")]
    [JsonPropertyName("Produtos")]
    public List<Produto> Produtos { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}