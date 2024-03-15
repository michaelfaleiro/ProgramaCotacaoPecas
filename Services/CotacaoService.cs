using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProgramaCotacaoPecas.Data;
using ProgramaCotacaoPecas.Models;
using ProgramaCotacaoPecas.ViewsModels;

namespace ProgramaCotacaoPecas.Services;

public class CotacaoService
{
    private readonly IMongoCollection<Cotacao> _mongoCollection;

    public CotacaoService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionUri);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _mongoCollection = database.GetCollection<Cotacao>(mongoDbSettings.Value.CollectionName["CotacaoCollection"]);
    }


    public async Task CreateAsync(Cotacao cotacao)
    {
        await _mongoCollection.InsertOneAsync(cotacao);
    }

    public async Task<List<Cotacao>> GetAsync()
    {
        return await _mongoCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Cotacao> GetById(string id)
    {
        return await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ListaProdutoViewModel> GetCotacaoWithProdutos(string id)
    {
        var pipeline = new BsonDocument[]
        {
            new("$match", new BsonDocument
            {
                { "_id", new ObjectId(id) }
            }),
            new("$lookup", new BsonDocument
            {
                { "from", "Produto" },
                { "localField", "produtoId._id" },
                { "foreignField", "_id" },
                { "as", "Produtos" }
            }),
            new("$project", new BsonDocument
            {
                {"produtoId", 0}
            })
        };

        var cotacao = await _mongoCollection.Aggregate<ListaProdutoViewModel>(pipeline).FirstOrDefaultAsync();

        return cotacao;
    }

    public async Task<Cotacao> Update(string cotacaoId, Cotacao cotacao)
    {
        var filter = Builders<Cotacao>.Filter.Eq(x => x.Id, cotacaoId);

        var update = Builders<Cotacao>.Update
            .Set(x => x.Carro, cotacao.Carro)
            .Set(x => x.Placa, cotacao.Placa)
            .Set(x => x.Observacao, cotacao.Observacao)
            .Set(x => x.Status, cotacao.Status)
            .Set(x => x.Chassi, cotacao.Chassi)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        var result = await _mongoCollection.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
            throw new InvalidOperationException($"Cotação não encontrada.");

        return cotacao;
    }

    public async Task AddProdutoCotacao(string cotacaoId, string produtoId)
    {
        var filter = Builders<Cotacao>.Filter.Eq(x => x.Id, cotacaoId);

        var update = Builders<Cotacao>.Update.AddToSet(x => x.ProdutoId, new ProdutoId { IdProduto = produtoId });

        var result = await _mongoCollection.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
            throw new InvalidOperationException($"Cotação não encontrada.");
    }

    public async Task RemoveProdutoCotacao(string cotacaoId, string produtoId)
    {
        var filter = Builders<Cotacao>.Filter.Eq(x => x.Id, cotacaoId);

        var update = Builders<Cotacao>.Update.Pull("produtos", produtoId);

        var result = await _mongoCollection.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
            throw new InvalidOperationException($"Cotação não encontrada.");
    }

    public async Task Delete(string cotacaoId)
    {
        var filter = Builders<Cotacao>.Filter.Eq(x => x.Id, cotacaoId);
        var result = await _mongoCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new InvalidOperationException($"Cotação não encontrada.");
    }
}