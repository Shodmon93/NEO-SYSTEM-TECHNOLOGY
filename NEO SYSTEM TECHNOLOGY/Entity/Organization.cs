namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Organization
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }
        public int PersonID { get; set; }


    }
}
