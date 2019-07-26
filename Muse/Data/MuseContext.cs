using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Muse.Models;

namespace Muse.Models
{
    public class MuseContext : DbContext
    {
        public MuseContext (DbContextOptions<MuseContext> options)
            : base(options)
        {
        }

        public DbSet<Muse.Models.User> User { get; set; }

        public DbSet<Muse.Models.Musing> Musing { get; set; }
    }
}
