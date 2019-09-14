using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NLayerApp.Models.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(ref ModelBuilder builder)
        {
            builder.ApplyConfiguration<Member>(this);
        }
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.Groups)
                .WithOne(g => g.Member);
        }
    }
}