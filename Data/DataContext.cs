using Ines_German.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ines_German.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<WordModel> Words { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}