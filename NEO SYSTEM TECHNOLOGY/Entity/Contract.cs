using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Contract
    {
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public int ContractSum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Currency Currency { get; set; }
        public bool IsVatIncluded { get; set; }

        public Organization Organization { get; set; }

    }
}
