using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Terra.Server.Models;

namespace Terra.Server.Data
{
    public class TerraServerContext : DbContext
    {
        public TerraServerContext (DbContextOptions<TerraServerContext> options)
            : base(options)
        {
        }

        public DbSet<Terra.Server.Models.Way> Way { get; set; } = default!;
    }
}
