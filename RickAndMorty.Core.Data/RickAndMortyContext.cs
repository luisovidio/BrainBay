using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data
{
    public class RickAndMortyContext : DbContext
    {
        public RickAndMortyContext(DbContextOptions<RickAndMortyContext> context)
            : base(context)
        {
       
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
