using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class RmDogovorOrganizationVM : BaseDogovorViewModel
    {
        public ICollection<Zakaz> Orders { get; set; }
        [Display(Name = "Номер Заказа")]
        public int OrderNumber { get; set; }



        public IEnumerable<RmDogovorOrganizationVM> GetAllRmDogovors(IEnumerable<Dogovor> dogovors)
        {
            var rmDogovors = dogovors.Select(dog => new RmDogovorOrganizationVM
            {
                DogovorId = dog.ID,
                OrganizationName = dog.Organization.Name,
                Orders = dog.Zakaz,
                OrderHeader = dog.OrderHeader,
                StartDate = dog.StartDate,
                EndDate = dog.EndDate,
                Currency = dog.Currency,
                DogovorSum = dog.DogovorSum,
                IsVatIncluded = dog.IsVatIncluded
            }); 

            return rmDogovors;
        }

        public Dogovor SaveRmDogovor(RmDogovorOrganizationVM rmDogovor, Organization organization)
        {
            var taxPrice = rmDogovor.DogovorSum + (rmDogovor.DogovorSum * TAX / 100);
            var totalPrice = rmDogovor.IsVatIncluded ? taxPrice : rmDogovor.DogovorSum;

            Dogovor dogovor = new Dogovor()
            {
                
                OrderHeader = rmDogovor.OrderHeader, // The order name for Organization
                DogovorSum = totalPrice, // The Amount of contract based on the TAX
                StartDate = rmDogovor.StartDate,
                EndDate = rmDogovor.EndDate,
                Currency = rmDogovor.Currency,
                IsVatIncluded = rmDogovor.IsVatIncluded,
                IsOneTimeDogovor = rmDogovor.IsOneTimeDogovor,

                Organization = organization, 
                Zakaz = new List<Zakaz>()
                {
                    new Zakaz()
                    {
                       OrderNumber = rmDogovor.OrderNumber,
                    }
                }
            };

            return dogovor;
        }
    }

}
