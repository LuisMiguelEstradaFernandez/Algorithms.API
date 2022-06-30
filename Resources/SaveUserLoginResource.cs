using System.ComponentModel.DataAnnotations;

namespace Algorithms.API.Resources
{
    public class SaveUserLoginResource
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
