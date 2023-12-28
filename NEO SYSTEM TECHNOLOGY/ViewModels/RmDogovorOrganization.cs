using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class RmDogovorOrganization
    {
        public int DogovorId { get; set; }
        public int OrganizationId { get; set; }
        public int ZakazId { get; set; }
        public string OrderHeader { get; set; }
        [Display(Name = "Сумма Договора")]
        public decimal DogovorSum { get; set; }

        [Display(Name = "Дата Договора")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата Окончания Договора")]
        public DateTime EndDate { get; set; }
        public Currency Currency { get; set; }

        [Display(Name = "Валюта")]
        public bool IsVatIncluded { get; set; } = false;

        public string OrganizationName { get; set; }

        [Display(Name = "Номер Заказа")]
        public int OrderNumber { get; set; }

        public bool IsOneTimeDogovor { get; set; }

        public ICollection<Zakaz> Orders {  get; set; }
    }

}
