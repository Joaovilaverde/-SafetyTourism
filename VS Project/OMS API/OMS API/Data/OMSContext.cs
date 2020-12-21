using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMS_API.Models;

namespace OMS_API.Data
{
    public class OMSContext : DbContext
    {
        public OMSContext (DbContextOptions<OMSContext> options)
            : base(options)
        {
        }

        public DbSet<OMS_API.Models.Surto> Surto { get; set; }

        public DbSet<OMS_API.Models.Zona> Zona { get; set; }
    }
}
