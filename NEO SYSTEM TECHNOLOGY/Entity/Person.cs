namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Organization Organization { get; set; }
        public int OrganizationID { get; set; }
    }
}
