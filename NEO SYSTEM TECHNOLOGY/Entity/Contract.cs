using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.Entity
{
    public class Contract
    {
        public int ID { get; set; }
        [Display(Name = "Номер Заказа")]
        public int OrderNumber { get; set; }

        [Display(Name = "Сумма Договора")]
        public decimal ContractSum { get; set; }

        [Display(Name = "Дата Договора")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата Окончания Договора")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }

        public bool IsVatIncluded { get; set; }

        public Organization Organization { get; set; }

    }
}
