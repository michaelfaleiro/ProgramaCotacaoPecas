using System.ComponentModel.DataAnnotations;
using ProgramaCotacaoPecas.Models;

namespace ProgramaCotacaoPecas.ViewsModels;

public class PrecoProdutoViewModel
{
    public string Fornecedor { get; set; }
    public string Sku { get; set; }
    public string Marca { get; set; }
    public double PrecoCusto { get; set; }
    public int QuantidadeAtendida { get; set; }
}