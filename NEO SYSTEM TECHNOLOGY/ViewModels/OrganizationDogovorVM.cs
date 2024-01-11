using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.Globalization;
using System.Text;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class OrganizationDogovorVM : BaseDogovorViewModel
    {
        public Zakaz Order { get; set; }
        public string FilePath { get; set; }
        public byte[] myPdfFile { get; set; }

        public IEnumerable<OrganizationDogovorVM> GetAllDogovors(IEnumerable<Dogovor> allDogovors)
        {
            var filteredDogovors = allDogovors
                .Where(dogovor => dogovor.IsOneTimeDogovor)
                .Select(dogovor => new OrganizationDogovorVM
                {
                    OrderHeader = dogovor.OrderHeader,
                    OrganizationName = dogovor.Organization.Name,
                    OrganizationId = dogovor.Organization.ID,
                    StartDate = dogovor.StartDate,
                    EndDate = dogovor.EndDate,
                    IsVatIncluded = dogovor.IsVatIncluded,
                    IsOneTimeDogovor = dogovor.IsOneTimeDogovor,
                    Currency = dogovor.Currency,
                    DogovorSum = dogovor.DogovorSum,
                    DogovorId = dogovor.ID,
                    FilePath = Convert.ToBase64String(dogovor.Content)

                }).ToList();
            return filteredDogovors;
        }

        public Dogovor GetDogovorToInsert(OrganizationDogovorVM dogovorVM, Organization organization)
        {
            var priceWithTax = dogovorVM.DogovorSum + (dogovorVM.DogovorSum * (TAX / 100.0));
            var finalDogovorSum = dogovorVM.IsVatIncluded ? priceWithTax : dogovorVM.DogovorSum;

            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileInfo fileInfo = new FileInfo(dogovorVM.DogFile.FileName);
            string fileName = fileInfo.Name + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                dogovorVM.DogFile.CopyTo(stream);
            }


            var memoryStream = new MemoryStream();

            dogovorVM.DogFile.CopyTo(memoryStream);

                Dogovor dogovor = new Dogovor()
                {
                    OrderHeader = dogovorVM.OrderHeader,
                    DogovorSum = finalDogovorSum,
                    StartDate = dogovorVM.StartDate,
                    EndDate = dogovorVM.EndDate,
                    Currency = dogovorVM.Currency,
                    IsVatIncluded = dogovorVM.IsVatIncluded,
                    IsOneTimeDogovor = dogovorVM.IsOneTimeDogovor,
                    Organization = organization,
                    Content = memoryStream.ToArray(),

                };
            return dogovor;
        }
        public Dogovor GetDogovorToUpdate(Dogovor dogovor, OrganizationDogovorVM dogovorVM)
        {
            var priceWithTax = dogovorVM.DogovorSum + (dogovorVM.DogovorSum * (TAX / 100.0));
            var finalDogovorSum = dogovorVM.IsVatIncluded ? priceWithTax : dogovorVM.DogovorSum;

            dogovor.OrderHeader = dogovorVM.OrderHeader;
            dogovor.DogovorSum = finalDogovorSum;
            dogovor.StartDate = dogovorVM.StartDate;
            dogovor.EndDate = dogovorVM.EndDate;
            dogovor.Currency = dogovorVM.Currency;
            dogovor.IsVatIncluded = dogovorVM.IsVatIncluded;
            return dogovor;
        }

        public OrganizationDogovorVM EditDogovor(Dogovor dogovor)
        {
            OrganizationDogovorVM dogovorInDb = new OrganizationDogovorVM()
            {
                DogovorId = dogovor.ID,
                OrganizationId = dogovor.Organization.ID,
                OrganizationName = dogovor.Organization.Name,
                OrderHeader = dogovor.OrderHeader,
                StartDate = dogovor.StartDate,
                EndDate = dogovor.EndDate,
                Currency = dogovor.Currency,
                DogovorSum = dogovor.DogovorSum,
                IsVatIncluded = dogovor.IsVatIncluded,

            };
            return dogovorInDb;
        }
    }
}
