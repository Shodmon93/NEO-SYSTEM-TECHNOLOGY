using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class RmDogovorOrganization : BaseDogovorViewModel
    {
        public ICollection<Zakaz> Orders {  get; set; }        
        [Display(Name = "Номер Заказа")]
        public int OrderNumber { get; set; }

    }

}
