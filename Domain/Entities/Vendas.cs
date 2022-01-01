using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vendas
    {
        public Vendas(long venId)
        {
            this.venId = venId;
        }
        public long venId { get; set; }
        public DateTime venDataVenda { get; } = DateTime.Now;
        public DateTime venDataAtualizacao { get; set; }
        public Guid cliId { get; set; }
        public virtual Cliente _cliente { get; set; }
        public List<VendasItens> _itens { get; set; }
    }
}
