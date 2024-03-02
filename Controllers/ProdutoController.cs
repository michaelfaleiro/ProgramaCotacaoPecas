using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProgramaCotacaoPecas.Models;
using ProgramaCotacaoPecas.Services;
using ProgramaCotacaoPecas.ViewsModels;

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
            return StatusCode(500, "Erro ao Buscar Dados!");
        }
        catch
        {
            return StatusCode(500, "Falha interna no Servidor!");
        }
    }

    [HttpPost("addpreco/{produtoId}")]
    public async Task<IActionResult> AddPrecoProduto([FromBody] Preco model, string produtoId)
    {
        try
        {
            await _produtoService.AddPrecoProduto(produtoId, model);

            return Ok();
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


    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateCotacaoAsync(string id, [FromBody] Produto model)
    {
        try
        {
            var result = await _produtoService.GetById(id);

            if (result == null)
                return BadRequest("Produto não encontrado");

            var produto = await _produtoService.Update(id, model);

            return Ok(produto);
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

            await _produtoService.DeleteCotacao(id);

            return NoContent();
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