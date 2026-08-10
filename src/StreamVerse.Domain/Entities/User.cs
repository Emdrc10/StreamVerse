using StreamVerse.Domain.Core;

namespace StreamVerse.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }
}
