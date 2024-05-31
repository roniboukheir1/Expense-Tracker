using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required]
        public int Amount { get; set; }

        [StringLength(50)]
        public string? Note { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}


