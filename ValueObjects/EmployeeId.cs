using TeamManagement.Domain.ddd;

namespace TeamManagement.ValueObjects
{
    public class EmployeeId(int id) : IEntityId
    {
        public int id { get; } = id;
    }
}
