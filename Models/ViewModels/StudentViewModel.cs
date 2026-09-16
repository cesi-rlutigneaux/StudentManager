using System.ComponentModel.DataAnnotations;

namespace StudentManager.Models.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        public bool Subscribed { get; set; }
    }
}
