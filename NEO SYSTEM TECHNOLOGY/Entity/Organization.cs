

using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Organization
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Employee> Person { get; set; } = new List<Employee>();
        public ICollection<Dogovor> Dogovors { get; set; } = new List<Dogovor>();


    }
}
