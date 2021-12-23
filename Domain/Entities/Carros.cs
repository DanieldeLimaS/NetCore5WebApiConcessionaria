using System;

namespace Domain.Entities
{
    public class Carros
    {


        public Guid carId { get; set; }
        public string carModelo { get; set; }
        public string carMarca { get; set; }
        public DateTime carAno { get; set; }
        public decimal? carPreco { get; set; }
        public bool carDisponivel { get; set; }
        public DateTime carDataCadastro { get; set; }
    }
}
