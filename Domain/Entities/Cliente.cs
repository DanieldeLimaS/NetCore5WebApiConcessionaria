using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente
    {
        public Cliente()
        {

        }
        public Guid cliId { get; set; }
        public string cliNome { get; set; }
        public string cliLogradouro { get; set; }
        public string cliNumero { get; set; }
        public string cliBairro { get; set; }
        public string cliCidade { get; set; }
        public string cliUf { get; set; }
        public string cliComplemento { get; set; }
        public bool cliAtivo { get; set; }
        public string cliAnotacoes { get; set; }
    }
}
