using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Dogovor
    {
        public int ID { get; set; }
        [Display(Name = "Договор")]
        public string OrderHeader { get; set; }

        [Display(Name = "Сумма Договора")]
        public decimal DogovorSum { get; set; }

        [Display(Name = "Дата Договора")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата Окончания Договора")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }

        public bool IsVatIncluded { get; set; }

        public Organization Organization { get; set; }

        public Receipt Receipt { get; set; }

    }
}
