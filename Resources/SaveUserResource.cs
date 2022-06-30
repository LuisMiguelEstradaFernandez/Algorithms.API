using System.ComponentModel.DataAnnotations;

namespace Algorithms.API.Resources
{
    public class SaveUserResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
