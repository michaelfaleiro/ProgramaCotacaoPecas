using System.ComponentModel.DataAnnotations;

namespace ProgramaCotacaoPecas.ViewsModels.CotacaoViewsModels;

public class EditorCotacaoViewModel
{
    [Required(ErrorMessage = "Informe o Carro")]
    public string Carro { get; set; } = null!;
    public string Placa { get; set; } = null!;

    [MaxLength(17, ErrorMessage = "Máximo 17 caracteres")]
    public string Chassi { get; set; } = null!;

    public List<string>? Produtos { get; set; } = [];
}