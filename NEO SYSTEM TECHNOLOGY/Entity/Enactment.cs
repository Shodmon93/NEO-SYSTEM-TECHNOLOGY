namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Enactment
    {
        public int ID { get; set; }
        public int AmountValue { get; set; }
        public DateTime EnactmentDate { get; set; }
        public DateTime ExposedNfsDate { get; set; }

        public Dogovor Contract { get; set; }
        public int ContractID { get; set; }

        public Nfs Nfs { get; set; }
        public int NfsID { get; set; }
    }
}
