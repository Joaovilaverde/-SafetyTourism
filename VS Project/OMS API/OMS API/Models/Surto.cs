using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS_API.Models
{
    public class Surto
    {
        public long SurtoID { get; set; }
        public long VirusID { get; set; }
        [ForeignKey ("VirusID")]
        public Virus Virus { get; set; }
        public long ZonaID { get; set; }
        [ForeignKey("ZonaID")]
        public Zona Zona { get; set; }
        public DateTime DataDetecao { get; set; }
        public DateTime DataFim { get; set; }
    }
}
