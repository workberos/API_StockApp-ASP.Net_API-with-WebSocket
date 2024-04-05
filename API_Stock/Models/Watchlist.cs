using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Stock.Models
{
    [Table("watchlists")]
    public class Watchlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [Key]
        [ForeignKey("Stock")]
        [Column("stock_id")]

        public int StockId { get; set; }

        
        public User? User { get; set; }
        public Stock? stock { get; set; }
    }
}
