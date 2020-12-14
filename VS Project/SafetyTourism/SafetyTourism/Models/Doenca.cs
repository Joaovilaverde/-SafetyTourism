using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyTourism.Models
{
    public class Doenca
    {
        public int DoencaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int RecomendacaoId { get; set; }
        public Recomendacao recomendacao { get; set; }
    }
}
