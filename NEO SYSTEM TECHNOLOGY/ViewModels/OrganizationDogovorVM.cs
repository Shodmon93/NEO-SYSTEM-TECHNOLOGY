using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class OrganizationDogovorVM
    {
        public int DogovorId { get; set; }
        public string OrderHeader { get; set; }
        public decimal DogovorSum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Currency Currency { get; set; }
        public bool IsVatIncluded { get; set; }
        public Receipt Receipt { get; set; }
        public Organization Organization { get; set; }
        public Dogovor Dogovor { get; set; }

        public string OrganizationName { get; set; }
        public int OrganizationId { get; set; }
    }
}
