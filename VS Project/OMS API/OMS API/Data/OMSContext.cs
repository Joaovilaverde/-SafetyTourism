using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace OMS_API.Data
{
    public class OMSContext :DbContext
    {
        public OMSContext(DbContextOptions<OMSContext> options) : base(options)
        {
        }
    }
}
