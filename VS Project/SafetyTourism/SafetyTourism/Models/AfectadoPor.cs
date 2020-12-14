using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyTourism.Models
{
    public enum Gravidade
    {
        Bom, Medio, Mau, MuitoMau, Pior, Péssimo, Apocalipse
    }
    public class AfectadoPor
    {

        public int AfectadoPorId { get; set; }
        public int DestinoId { get; set; }
        public Destino Destino { get; set; }
        public int DoencaId { get; set; }
        public Doenca Doenca { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public Gravidade? Gravidade { get; set; }

        [Range(0, 100000)]
        public int InfectadosPor100k { get; set; }
        
    }
}
