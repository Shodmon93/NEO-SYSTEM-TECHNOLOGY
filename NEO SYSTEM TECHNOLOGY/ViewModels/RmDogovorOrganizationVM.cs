using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class RmDogovorOrganizationVM : BaseDogovorViewModel
    {
        public ICollection<Zakaz> Orders { get; set; }
        [Display(Name = "Номер Заказа")]
        public int OrderNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderEndDate { get; set; }
        public double OrderSum { get; set; }
        public string PathFile { get; set; }





        public IEnumerable<RmDogovorOrganizationVM> GetAllRmDogovors(IEnumerable<Dogovor> dogovors)
        {
            var rmDogovors = dogovors.Select(dog => new RmDogovorOrganizationVM
            {
                DogovorId = dog.ID,
                OrganizationName = dog.Organization.Name,
                OrderHeader = dog.OrderHeader,
                StartDate = dog.StartDate,
                EndDate = dog.EndDate,
                Currency = dog.Currency,
                DogovorSum = dog.DogovorSum,
                IsVatIncluded = dog.IsVatIncluded
            }).ToList(); 

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

        public RmDogovorOrganizationVM GetDogovor(Dogovor rmDogovor)
        {
            RmDogovorOrganizationVM dogovorZakazOrganization = new RmDogovorOrganizationVM
            {
                DogovorId = rmDogovor.ID,
                OrganizationName = rmDogovor.Organization.Name,
                OrderHeader = rmDogovor.OrderHeader,
                StartDate = rmDogovor.StartDate.Date,
                EndDate = rmDogovor.EndDate,
                DogovorSum = rmDogovor.DogovorSum,
                Orders = rmDogovor.Zakaz
            };
            return dogovorZakazOrganization;
        }

        public RmDogovorOrganizationVM AddNewZakaz(Dogovor dogovor)
        {
            RmDogovorOrganizationVM newZakaz = new RmDogovorOrganizationVM
            {
                DogovorId = dogovor.ID,
                OrganizationName = dogovor.Organization.Name,
                OrderHeader = dogovor.OrderHeader,
                StartDate = dogovor.StartDate.Date,
                EndDate = dogovor.EndDate,

            };

            return newZakaz;
        }

        public Zakaz GetNewZakaz (RmDogovorOrganizationVM newZakaz)
        {
            Zakaz newOrder = new Zakaz()
            {
                OrderNumber = newZakaz.OrderNumber,
                StartDate = newZakaz.OrderStartDate,
                EndDate = newZakaz.OrderEndDate,
                ZakazSum = newZakaz.OrderSum
            };
            return newOrder;
        }

    }

}
