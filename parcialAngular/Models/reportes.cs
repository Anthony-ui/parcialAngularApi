using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parcialAngular.Models
{
    public class reportes
    {

        public int idEvento { get; set; }
        public DateTime? fecha { get; set; }
        public string evento { get; set; }
        public string lugar { get; set; }
        public decimal costo { get; set; }
        public string nombre { get; set; }
        public int dias { get; set; }
      
    }
}
