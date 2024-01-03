using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class BaseDogovorViewModel
    {
        protected const double TAX = 15.0;
        public int DogovorId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int ZakazId { get; set; }


        [Display(Name = "Договор")]
        public string OrderHeader { get; set; }
        [Display(Name = "Сумма Договора")]
        public double DogovorSum { get; set; }
        [Display(Name = "Дата Договора")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата Окончания Договора")]
        public DateTime EndDate { get; set; }
        public Currency Currency { get; set; }
        [Display(Name = "Валюта")]
        public bool IsVatIncluded { get; set; } = false;
        public bool IsOneTimeDogovor { get; set; }

        public Receipt Receipt { get; set; }
        public Organization Organization { get; set; }
        public Dogovor Dogovor { get; set; }




        public BaseDogovorViewModel GetOrganization(Organization organization, bool isOneTimeDogovor)
        {
            if (isOneTimeDogovor)
            {
                OrganizationDogovorVM organizationDogovorVM = new OrganizationDogovorVM()
                {
                    OrganizationId = organization.ID,
                    OrganizationName = organization.Name,
                    IsOneTimeDogovor = isOneTimeDogovor
                };
                return organizationDogovorVM;
            }
            else
            {
                RmDogovorOrganization rmDogovorOrganization = new RmDogovorOrganization()
                {
                    OrganizationId = organization.ID,
                    OrganizationName = organization.Name,
                    IsOneTimeDogovor = isOneTimeDogovor
                };
                return rmDogovorOrganization;                
            }
        }
    }
}
