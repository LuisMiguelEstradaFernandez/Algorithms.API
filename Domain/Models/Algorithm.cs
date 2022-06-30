namespace Algorithms.API.Domain.Models
{
    public class Algorithm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with User Entity
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
