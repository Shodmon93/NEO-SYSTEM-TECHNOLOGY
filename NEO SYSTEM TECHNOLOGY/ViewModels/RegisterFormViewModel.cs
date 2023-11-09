using Microsoft.AspNetCore.Mvc.Rendering;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class RegisterFormViewModel
    {
        public Organization Organization { get; set; }
        public Person Person { get; set; }
        public Contract Contract { get; set; }
        public Receipt Receipt { get; set; }
        public Invoice Invoice { get; set; }
        public Enactment Enactment { get; set; }


        public IEnumerable<SelectListItem> ContractType
        {
            get
            {
                return Enum.GetNames(typeof(ContractType)).Select(p => new SelectListItem() { Text = p, Value = p });
            }
        }

    }
}


