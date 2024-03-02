using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProgramaCotacaoPecas.Data;
using ProgramaCotacaoPecas.Models;

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

    public async Task<Cotacao> Update(string id, Cotacao cotacao)
    {
        var filter = Builders<Cotacao>.Filter.Eq("Id", id);

        var update = Builders<Cotacao>.Update
            .Set(x => x.Carro, cotacao.Carro)
            .Set(x => x.Chassi, cotacao.Chassi);

        await _mongoCollection.UpdateOneAsync(filter, update);

        return cotacao;
    }

    public async Task AddProdutoCotacao(string cotacaoId, string produtoId)
    {
        var filter = Builders<Cotacao>.Filter.Eq("Id", cotacaoId);

        var update = Builders<Cotacao>.Update.AddToSet("produtos", produtoId);

        await _mongoCollection.UpdateOneAsync(filter, update);
    }

    public async Task RemoveProdutoCotacao(string cotacaoId, string produtoId)
    {
        var filter = Builders<Cotacao>.Filter.Eq("Id", cotacaoId);

        var update = Builders<Cotacao>.Update.Pull("produtos", produtoId);

        await _mongoCollection.UpdateOneAsync(filter, update);
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Cotacao>.Filter.Eq("Id", id);
        await _mongoCollection.DeleteOneAsync(filter);
    }
}