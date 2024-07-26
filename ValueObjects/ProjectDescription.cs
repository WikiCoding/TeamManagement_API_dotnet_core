namespace TeamManagement.ValueObjects
{
    public class ProjectDescription
    {
        private string description;

        public ProjectDescription(string description)
        {
            Description = description;
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value == null || value.Trim().Length == 0 )
                {
                    throw new ArgumentException("Description can't be null or empty");
                }

                if (value.Length > 140)
                {
                    throw new ArgumentException("Description is more than 140 characters");
                }
                
                description = value.Trim();
            }
        }
    }
}
