namespace Algorithms.API.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with Algorithm Entity
        public IList<Algorithm> Algorithms { get; set; } = new List<Algorithm>();

        // Relationship with UserLogin Entity
        public Guid? UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }
    }
}
