using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prj.TaskManager.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string  FullName { get; set; }
    }
}
