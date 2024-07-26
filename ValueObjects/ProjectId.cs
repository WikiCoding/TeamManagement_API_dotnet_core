using TeamManagement.Domain.ddd;

namespace TeamManagement.ValueObjects
{
    public class ProjectId(int id) : IEntityId
    {
        public int id { get; } = id;
    }
}
