using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RickAndMorty.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Data.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Sk);

            builder.HasOne(x => x.Origin)
                .WithMany()
                .HasForeignKey(x => x.OriginId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Location)
                .WithMany(l => l.Residents)
                .HasForeignKey(x => x.LocationId);

            builder.HasMany(x => x.Episodes)
                .WithMany(e => e.Characters);

        }
    }
}
