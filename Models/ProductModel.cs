using CommunityToolkit.Mvvm.ComponentModel;
using SaveUp.Library.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveUp.Models
{
    [Table("Products")]
    public class ProductModel : BaseModel
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(19,2)")]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy, hh:mm}")]
        public DateTime DateTimeAdded { get; set; } = DateTime.Now;
    }
}
