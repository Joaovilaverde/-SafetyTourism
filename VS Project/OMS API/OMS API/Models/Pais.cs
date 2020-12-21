using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS_API.Models
{
    public class Pais
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public long ZonaId { get; set; }
        [ForeignKey("ZonaId")]
        public Zona Zona { get; set; }

    }
}
