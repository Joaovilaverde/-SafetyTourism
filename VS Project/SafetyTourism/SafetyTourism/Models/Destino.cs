using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyTourism.Models
{
    public class Destino
    {
        public int DestinoId { get; set; }
        public string Nome { get; set; }
        public ICollection<AfectadoPor> Afectados { get; set; }
    }
}
