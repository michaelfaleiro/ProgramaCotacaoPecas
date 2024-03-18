using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramaCotacaoPecas.ValueObj
{
    public class Carro
    {
        [Required(ErrorMessage = "Informe o Carro")]
        public string Name { get; set; } = null!;

        [MaxLength(7, ErrorMessage = "Máximo 7 caracteres")]
        public string? Placa { get; set; }

        [MaxLength(17, ErrorMessage = "Máximo 17 caracteres")]
        public string? Chassi { get; set; }
    }
}