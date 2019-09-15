using Microsoft.EntityFrameworkCore.Design;
using NLayerApp.Models;
using System;

namespace NLayerApp.DataAccessLayer
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var types =
                new Type[] {
                    typeof(Member), typeof(Group),
                    typeof(GroupMembers),
                    typeof(Subject), typeof(Room)};

            return new AppDbContext(@"Server=.\;Initial Catalog=nlayerappdb;Integrated Security=True;", types);
        }
    }
}