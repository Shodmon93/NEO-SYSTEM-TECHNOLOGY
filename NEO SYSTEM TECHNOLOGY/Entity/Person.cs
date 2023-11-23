using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Person
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public Organization Organization { get; set; }
        public int OrganizationID { get; set; }
    }
}
