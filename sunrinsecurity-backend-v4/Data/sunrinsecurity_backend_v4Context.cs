using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sunrinsecurity_backend_v4.Models;

namespace sunrinsecurity_backend_v4.Data
{
    public class sunrinsecurity_backend_v4Context : DbContext
    {
        public sunrinsecurity_backend_v4Context (DbContextOptions<sunrinsecurity_backend_v4Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<sunrinsecurity_backend_v4.Models.Notice> Notice { get; set; } = default!;

        public DbSet<sunrinsecurity_backend_v4.Models.Club> Club { get; set; } = default!;

        public DbSet<sunrinsecurity_backend_v4.Models.Project> Project { get; set; } = default!;

        public DbSet<sunrinsecurity_backend_v4.Models.Response> Response { get; set; } = default!;

        public DbSet<sunrinsecurity_backend_v4.Models.Application> Application { get; set; } = default!;
    }
}
