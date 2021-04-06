using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProPendencia.Api.Model;

    public class PendenciaContext : DbContext
    {
        public PendenciaContext (DbContextOptions<PendenciaContext> options)
            : base(options)
        {
        }

        public DbSet<ProPendencia.Api.Model.Pendencia> Pendencia { get; set; }
    }
