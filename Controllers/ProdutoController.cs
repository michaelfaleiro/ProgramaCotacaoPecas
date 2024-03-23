using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ProgramaCotacaoPecas.Models;
using ProgramaCotacaoPecas.Services;


namespace ProgramaCotacaoPecas.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutoController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetProdutos()
    {
        try
        {
            var cotacoes = await _produtoService.GetAsync();

            return Ok(cotacoes);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Erro ao Buscar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetByIdProduto(string id)
    {
        try
        {
            var produto = await _produtoService.GetById(id);

            return Ok(produto);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Erro ao Buscar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> NewProduto([FromBody] Produto model)
    {
        try
        {
            await _produtoService.CreateAsync(model);
            return Created($"produtos/{model.Id}", model);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Erro ao Salvar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpPost("{produtoId:length(24)}/addpreco")]
    public async Task<IActionResult> AddPrecoProduto([FromBody] Preco model, string produtoId)
    {
        model.Id = ObjectId.GenerateNewId().ToString();
        try
        {
            await _produtoService.AddPrecoProduto(produtoId, model);

            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Erro ao Salvar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpPut("{produtoId:length(24)}/updatepreco")]
    public async Task<IActionResult> UpdatePrecoProduto([FromBody] Preco model, string produtoId)
    {
        try
        {
            await _produtoService.UpdatePrecoProduto(produtoId, model);

            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Erro ao Atualizar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpDelete("{produtoId:length(24)}/removepreco/{precoId:length(24)}")]
    public async Task<IActionResult> RemovePrecoProduto(string produtoId, string precoId)
    {
        try
        {
            await _produtoService.RemovePrecoProduto(produtoId, precoId);

            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Erro ao Remover Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }


    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateCotacaoAsync(string id, [FromBody] Produto model)
    {
        try
        {
            var produto = await _produtoService.Update(id, model);

            return Ok(produto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Falha ao Atualizar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteCotacao(string id)
    {
        try
        {
            var result = await _produtoService.GetById(id);

            if (result == null)
                return BadRequest("Produto não encontrado");

            await _produtoService.Delete(id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (MongoWriteException)
        {
            return StatusCode(500, "Falha ao Remover Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }
}