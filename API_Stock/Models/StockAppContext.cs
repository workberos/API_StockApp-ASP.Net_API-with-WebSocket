using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace API_Stock.Models
{
    public class StockAppContext : DbContext
    {
        public StockAppContext(DbContextOptions<StockAppContext> options) 
            : base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<RealtimeQuote> RealtimeQuotes { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<CoveredWarrant> CoveredWarrants { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Watchlist>().HasKey(w => new { w.UserId, w.StockId });
            modelBuilder.Entity<Order>().ToTable(table => table.HasTrigger("trigger_orders"));
        }
    }
}
