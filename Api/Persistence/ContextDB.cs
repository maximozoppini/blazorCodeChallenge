using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }

        public DbSet<Card> Card { get; set; }
    }
}