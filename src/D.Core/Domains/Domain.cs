namespace D
{
    public class Domain
    {
        public Domain(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public Domain(string name)
        {
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }

        public Env Environment { get; set; }

        // Members
    } 
}