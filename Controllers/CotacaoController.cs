using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProgramaCotacaoPecas.Models;
using ProgramaCotacaoPecas.Services;
using ProgramaCotacaoPecas.ViewsModels;

namespace ProgramaCotacaoPecas.Controllers;

[ApiController]
[Route("cotacoes")]
public class CotacaoController : ControllerBase
{
    private readonly CotacaoService _cotacaoService;

    public CotacaoController(CotacaoService cotacaoService)
    {
        _cotacaoService = cotacaoService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCotacao()
    {
        try
        {
            var cotacoes = await _cotacaoService.GetAsync();

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
    public async Task<IActionResult> GetByIdCotacao(string id)
    {
        try
        {
            var cotacao = await _cotacaoService.GetById(id);

            return Ok(cotacao);
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
    public async Task<IActionResult> NewCotacao([FromBody] Cotacao model)
    {
        try
        {
            await _cotacaoService.CreateAsync(model);
            return Created($"cotacoes/{model.Id}", model);
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

    [HttpPost("addproduto")]
    public async Task<IActionResult> AddProdutoCotacao([FromBody] ProdutoCotacaoViewModel model)
    {
        try
        {
            await _cotacaoService.AddProdutoCotacao(model.CotacaoId, model.ProdutoId);

            return Ok();
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

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateCotacaoAsync(string id, [FromBody] Cotacao model)
    {
        try
        {
            var result = await _cotacaoService.GetById(id);

            if (result == null)
                return BadRequest("Cotação não encontrada");

            var cotacao = await _cotacaoService.Update(id, model);

            return Ok(cotacao);
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

    [HttpDelete("removeproduto")]
    public async Task<IActionResult> RemoveProdutoCotacao([FromBody] ProdutoCotacaoViewModel model)
    {
        try
        {
            await _cotacaoService.RemoveProdutoCotacao(model.CotacaoId, model.ProdutoId);

            return Ok();
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

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteCotacao(string id)
    {
        try
        {
            var result = await _cotacaoService.GetById(id);

            if (result == null)
                return BadRequest("Cotação não encontrada");

            await _cotacaoService.Delete(id);

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