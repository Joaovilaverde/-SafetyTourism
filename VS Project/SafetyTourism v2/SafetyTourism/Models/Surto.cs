using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Surto
    {
        public long Id { get; set; }
        public long VirusId { get; set; }
        [ForeignKey("VirusId")]
        public Virus Virus { get; set; }
        public string ZonaId { get; set; }
        [ForeignKey("ZonaId")]
        public Zona Zona { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataDetecao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataFim { get; set; }
    }
}
