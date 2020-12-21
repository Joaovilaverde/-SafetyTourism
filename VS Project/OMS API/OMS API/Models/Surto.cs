using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OMS_API.Models
{
    public class Surto
    {
        public long SurtoId { get; set; }
        public long VirusId { get; set; }
        [ForeignKey ("VirusId")]
        public Virus Virus { get; set; }
        public string ZonaId { get; set; }
        [ForeignKey("ZonaId")]
        public Zona Zona { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataDetecao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }
    }
}
