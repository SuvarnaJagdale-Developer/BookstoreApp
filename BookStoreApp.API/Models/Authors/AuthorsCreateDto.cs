using Microsoft.Build.Framework;

using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Authors
{
    public class AuthorsCreateDto
    {
        [Microsoft.Build.Framework.Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Microsoft.Build.Framework.Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Bio { get;set; }
    }
}
