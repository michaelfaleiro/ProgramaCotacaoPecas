using System.ComponentModel.DataAnnotations;

namespace ProgramaCotacaoPecas.ViewsModels;

public class ProdutoCotacaoViewModel
{
    [Required(ErrorMessage = "CotacaoId é obrigatório")]
    public string CotacaoId { get; set; }

    [Required(ErrorMessage = "ProdutoId é obrigatório")]
    public string ProdutoId { get; set; }
}