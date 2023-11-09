using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.Web.Mvc;

namespace NEO_SYSTEM_TECHNOLOGY.Models
{
    public class OrganizationRegisterViewModel
    {
        public Organization Organization { get; set; }

        public string SelectedContractType { get; set; }
        public IEnumerable<SelectListItem> Contract { get; set; }


    }
}
