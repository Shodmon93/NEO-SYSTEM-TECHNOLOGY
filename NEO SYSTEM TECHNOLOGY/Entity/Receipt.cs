using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Receipt
    {
        public int ID { get; set; }
        public int AccountNumber { get; set; }
        public int AmountFaceValue { get; set; }
        public Boolean IsVatIncluded { get; set; }
        public Currency Currency { get; set; }
        public int PaymentSum { get; set; }
        public DateTime DateFaceValue { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Content { get; set; }

        public Dogovor Dogovor { get; set; }
        public int DogovorId { get; set; }


    }
}
