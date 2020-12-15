using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyTourism.Models
{
    public class Surto
    {

        public int SurtoId { get; set; }
        public int DestinoId { get; set; }
        public Destino Destino { get; set; }
        public int DoencaId { get; set; }
        public Doenca Doenca { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Range(0, 100000)]
        public int InfectadosPor100k { get; set; }

        //public string Gravidade { get; set; }

        private string gravidade;
        public string Gravidade
        {
            get { return gravidade; }
            set { gravidade = DefinirGravidade(InfectadosPor100k); }
        }

        public string DefinirGravidade(int infectados)
        {
            string gravidade ="";
            if (infectados>=0 && infectados <= 5)
            {
                gravidade = "Excelente";
            }
            if (infectados >= 6 && infectados <= 25)
            {
                gravidade = "Bom";
            }
            if (infectados >= 26 && infectados <= 50)
            {
                gravidade = "Médio";
            }
            if (infectados >= 51 && infectados <= 100)
            {
                gravidade = "Mau";
            }
            if (infectados >= 101 && infectados <= 500)
            {
                gravidade = "Muito Mau";
            }
            if (infectados >= 501 && infectados <= 5000)
            {
                gravidade = "Pior";
            }
            if (infectados >= 5001 && infectados <= 50000)
            {
                gravidade = "Péssimo";
            }
            if (infectados >= 50001 && infectados <= 100000)
            {
                gravidade = "Apocalipse";
            }
            return gravidade;
        }
        
    }
}
