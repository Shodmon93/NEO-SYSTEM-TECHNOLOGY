using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Zakaz
    {
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double ZakazSum { get; set; }

        public Dogovor Dogovor { get; set; }
        public int DogovorId { get; set; }
    }
}
