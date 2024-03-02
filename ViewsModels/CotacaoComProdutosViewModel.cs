namespace ProgramaCotacaoPecas.ViewsModels;

public class CotacaoComProdutosViewModel
{
    public string Id { get; set; }
    public string Carro { get; set; }
    public string Chassi { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ProdutoDetalhadoViewModel> ProdutosDetalhados { get; set; }
}

public class ProdutoDetalhadoViewModel
{
    public string Id { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public List<PrecoDetalhadoViewModel> PrecosDetalhados { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class PrecoDetalhadoViewModel
{
    public string Id { get; set; }
    public string Fornecedor { get; set; }
    public string Sku { get; set; }
    public string Marca { get; set; }
    public decimal PrecoCusto { get; set; }
    public int QuantidadeAtendida { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}