﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Stock.Models
{
    [Table("quotes")]
    public class Quote
    {
        [Key]
        [Column("quote_id")]
        public int QuoteId { get; set; }

        [ForeignKey("stock")]
        [Column("stock_id")]
        public int StockId { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("change")]
        public decimal Change { get; set; }
        [Column("percent_change")]
        public decimal PercentChange { get; set; }
        [Column("volume")]
        public int Volume { get; set; }
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        // Navigation
        public Stock? Stock { get; set; }

    }
}
