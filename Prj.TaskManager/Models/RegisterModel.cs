using System.ComponentModel.DataAnnotations;

namespace Prj.TaskManager.Models
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
