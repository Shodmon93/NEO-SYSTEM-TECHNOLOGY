namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Invoice
    {
        public int ID { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceSum { get; set; }

        public Dogovor Contract { get; set; }
        public int ContractId { get; set; }

    }
}
