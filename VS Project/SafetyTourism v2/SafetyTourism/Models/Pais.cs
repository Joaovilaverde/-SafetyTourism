using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Pais
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string ZonaId { get; set; }
        [ForeignKey("ZonaId")]
        public Zona Zona { get; set; }
    }
}
