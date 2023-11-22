

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Organization
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Person> Person { get; set; } = new List<Person>();
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();


    }
}
