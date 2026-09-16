using System.ComponentModel.DataAnnotations;

namespace StudentManager.Models.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
