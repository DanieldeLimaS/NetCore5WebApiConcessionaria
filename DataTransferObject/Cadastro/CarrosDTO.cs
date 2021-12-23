using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Cadastro
{
    public class CarrosDTO
    {
        [Required]
        public string carModelo { get; set; }
        public string carMarca { get; set; }
        public DateTime carAno { get; set; }
        public decimal? carPreco { get; set; }
        public bool carDisponivel { get; set; }
        public DateTime carDataCadastro { get; set; }
    }
}
