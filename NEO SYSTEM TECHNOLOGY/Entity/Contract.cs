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
        public ContractType ContractType { get; set; }

        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }

        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }

        public Enactment Enactment { get; set; }
        public int EnactmentID { get; set; }

        public Invoice Invoice { get; set; }
        public int InvoiceID { get; set; }


    }
}
