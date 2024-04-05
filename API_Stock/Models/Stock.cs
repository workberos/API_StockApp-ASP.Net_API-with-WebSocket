using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Stock.Models
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("stock_id")]
        public int StockId { get; set; }

        [Required]
        [StringLength(10)]
        [Column("symbol")]

        public string? Symbol { get; set; }

        [Required]
        [StringLength(255)]
        [Column("company_name")]
        public string? CompanyName { get; set; }
        [Column("market_cap")]
        public decimal? MarketCap { get; set; }

        [StringLength(200)]
        [Column("sector")]
        public string? Sector { get; set; }

        [StringLength(200)]
        [Column("industry")]
        public string? Industry { get; set; }

        [StringLength(200)]
        [Column("sector_en")]
        public string? SectorEn { get; set; }

        [StringLength(200)]
        [Column("industry_en")]
        public string? IndustryEn { get; set; }

        [StringLength(50)]
        [Column("stock_type")]
        public string? StockType { get; set; }

        [Column("rank")]
        public int Rank { get; set; }

        [StringLength(200)]
        [Column("rank_source")]
        public string? RankSource { get; set; }

        [StringLength(255)]
        [Column("reason")]
        public string? Reason { get; set; }

        // Navigation property for Watchlist entries (optional)
        public ICollection<Watchlist>? Watchlists { get; set; }
    }
}
