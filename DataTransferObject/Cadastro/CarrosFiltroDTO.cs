using System;

namespace DataTransferObject.Cadastro
{
    public class CarrosFiltroDTO 
    {
        public decimal? carPrecoMenor { get; set; }
        public decimal? carPrecoMaior { get; set; }
        public DateTime? carDataCadastroIni { get; set; }
        public DateTime? carDataCadastroFim { get; set; }
        public DateTime? carAnoIni { get; set; }
        public DateTime? carAnoFim { get; set; }
        public string? carModelo { get; set; }
        public string carMarca { get; set; }
        public bool? carDisponivelVenda { get; set; }
    }
}
