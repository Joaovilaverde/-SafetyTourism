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
    public class Relatorio
    {
        public int RelatorioId { get; set; }
        public int DestinoId { get; set; }
        public string Doenca { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public Gravidade? Gravidade { get; set; }
        public int InfectadosPor100k { get; set; }

        public Destino Destino { get; set; }
    }
}
