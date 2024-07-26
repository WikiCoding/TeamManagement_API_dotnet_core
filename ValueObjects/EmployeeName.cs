using TeamManagement.Domain.ddd;

namespace TeamManagement.ValueObjects
{
    public class EmployeeName : IValueObject
    {
        private string name;

        public EmployeeName(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return name; }

            set
            {
                if (value == null || value.Trim().Length == 0)
                {
                    throw new ArgumentNullException("Name is null or empty");
                }

                name = value.Trim();
            }
        }
    }
}
