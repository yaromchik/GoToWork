using System.ComponentModel.DataAnnotations;

namespace GoToWork_Models.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
