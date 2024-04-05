using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Stock.Models
{
    [Table("covered_warrants")]
    public class CoveredWarrant
    {
        [Key]
        [Column("warrant_id")]
        public int WarrentId {  get; set; }

        [Column("name")]
        public string? Name { get; set; }
        [ForeignKey("Stock")]
        [Column("underlying_asset_id")]
        public int UnderlyingAssetId { get; set; }
        public Stock? Stock { get; set; }
        [Column("issue_date")]
        public DateTime? IssueDate {  get; set; }
        [Column("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        [Column("strike_price")]
        public decimal? StrikePrice { get; set; }
        [Column("warrant_type")]
        public string? Price { get; set;}
    }
}
