using StreamVerse.Domain.Core;

namespace StreamVerse.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
