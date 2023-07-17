using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Books
{
    public class BookUpdateDTO:BaseDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [Range(1800, int.MaxValue)]
        public int? Year { get; set; }

        [Required]
        public string Isbn { get; set; } = null!;
        [Required]
        [StringLength(250, MinimumLength = 100)]
        public string Summary { get; set; } = null!;
        public string Image { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue)]
        public double? Price { get; set; }
    }
}
