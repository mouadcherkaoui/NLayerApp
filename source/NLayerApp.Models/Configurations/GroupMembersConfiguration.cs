using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Models;

namespace NLayerApp.Models.Configurations
{
    public class GroupMembersConfiguration : IEntityTypeConfiguration<GroupMembers>
    {
        public void Configure(EntityTypeBuilder<GroupMembers> builder)
        {
            builder.HasKey(g => new { g.GroupId, g.MemberId} );
            // builder.HasAlternateKey(g => g.MemberId);
        }
    }
}