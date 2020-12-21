using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS_API.Models
{
    public class Zona
    {
        public string Id { get; set; }

        public string Nome { get; set; }
    }
}
