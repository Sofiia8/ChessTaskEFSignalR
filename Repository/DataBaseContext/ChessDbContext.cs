using ChessTaskEFSignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessTaskEFSignalR.Repository.DataBaseContext
{
    public class ChessDbContext : DbContext
    {
        public DbSet<Step> Steps { get; set; } = null!;
        private string _dbConnectionString;
        public ChessDbContext(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_dbConnectionString);
    }
}
