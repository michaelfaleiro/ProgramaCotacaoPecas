using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProgramaCotacaoPecas.Data;
using ProgramaCotacaoPecas.Models;

namespace ProgramaCotacaoPecas.Services;

public class ProdutoService
{
    private readonly IMongoCollection<Produto> _mongoCollection;

    public ProdutoService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionUri);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _mongoCollection = database.GetCollection<Produto>(mongoDbSettings.Value.CollectionName["ProdutoCollection"]);
    }

    public async Task<List<Produto>> GetAsync()
    {
        return await _mongoCollection.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(Produto produto)
    {
        await _mongoCollection.InsertOneAsync(produto);
    }

    public async Task<Produto> GetById(string id)
    {
        return await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Produto> Update(string id, Produto produto)
    {
        var filter = Builders<Produto>.Filter.Eq("Id", id);

        var update = Builders<Produto>.Update
            .Set(x => x.NomeProduto, produto.NomeProduto)
            .Set(x => x.Quantidade, produto.Quantidade)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        var result = await _mongoCollection.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
            throw new InvalidOperationException($"Produto não encontrado.");

        return produto;
    }

    public async Task AddPrecoProduto(string produtoId, Preco preco)
    {
        var filter = Builders<Produto>.Filter.Eq("Id", produtoId);

        var update = Builders<Produto>.Update.AddToSet("precos", preco);

        var result = await _mongoCollection.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
            throw new InvalidOperationException($"Produto não encontrado.");
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Produto>.Filter.Eq("Id", id);
        var result = await _mongoCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new InvalidOperationException($"Produto não encontrado.");
    }
}