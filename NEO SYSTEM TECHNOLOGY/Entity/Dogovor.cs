using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Dogovor
    {
        public int ID { get; set; }
        
        public string OrderHeader { get; set; }

        
        public double DogovorSum { get; set; }

       
        public DateTime StartDate { get; set; }

        
        public DateTime EndDate { get; set; }

        public Currency Currency { get; set; }

        public string FilePath { get; set; }

        public byte[] Content { get; set; }

        public bool IsVatIncluded { get; set; }

        public bool IsOneTimeDogovor { get; set; }

        public Organization Organization { get; set; }

        public Receipt Receipt { get; set; }

        public ICollection<Zakaz> Zakaz {  get; set; } = new List<Zakaz>();


    }
}
