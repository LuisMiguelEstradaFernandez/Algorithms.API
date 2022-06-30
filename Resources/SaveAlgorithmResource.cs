using System.ComponentModel.DataAnnotations;

namespace Algorithms.API.Resources
{
    public class SaveAlgorithmResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
