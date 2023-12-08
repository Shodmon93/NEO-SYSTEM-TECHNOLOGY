using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;



namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class DogovorController : Controller
    {
      private const decimal TAX = 15;

        private readonly ApplicationDbContext _context;

        public DogovorController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            var dogovorList = _context.Dogovors.Include(p => p.Organization).ToList();

            return View(dogovorList);
        }

        public IActionResult AddNewDogovor(int id)
        {
            var organization = _context.Organizations.SingleOrDefault(p => p.ID == id);
            var viewModel = new OrganizationDogovorVM()
            {
                Organization = organization
            };

            return View("DogovorForm", viewModel);
        }
        public IActionResult Save(OrganizationDogovorVM viewModel) 
        {
            var organizationInDb = _context.Organizations.Single(p => p.ID == viewModel.Organization.ID);

            if (!ModelState.IsValid)
            {
                var vm = new OrganizationDogovorVM
                {
                    Organization = organizationInDb,
                    Dogovor = viewModel.Dogovor
                };
                return View("DogovorForm", vm);
            }
            var priceWithTax = viewModel.Dogovor.DogovorSum + (viewModel.Dogovor.DogovorSum * ( TAX / 100));

            if (viewModel.Dogovor.ID == 0)
            {
                if (viewModel.Dogovor.IsVatIncluded)
                {
                    viewModel.Dogovor.DogovorSum = priceWithTax;
                }

                Dogovor contract = new Dogovor
                {
                    OrderHeader = viewModel.Dogovor.OrderHeader,
                    DogovorSum = viewModel.Dogovor.DogovorSum,
                    StartDate = viewModel.Dogovor.StartDate,
                    EndDate = viewModel.Dogovor.EndDate,
                    Currency = viewModel.Dogovor.Currency,
                    IsVatIncluded = viewModel.Dogovor.IsVatIncluded,
                    Organization = organizationInDb
                };

                _context.Dogovors.Add(contract);
            }
            else
            {
                var contractInDb = _context.Dogovors.Single(p => p.ID == viewModel.Dogovor.ID);
                contractInDb.OrderHeader = viewModel.Dogovor.OrderHeader;
                contractInDb.DogovorSum =  viewModel.Dogovor.DogovorSum;
                contractInDb.StartDate = viewModel.Dogovor.StartDate;
                contractInDb.EndDate = viewModel.Dogovor.EndDate;
                contractInDb.Currency = viewModel.Dogovor.Currency;
                contractInDb.IsVatIncluded = viewModel.Dogovor.IsVatIncluded;
            }
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Dogovor");
        }

        public IActionResult DogovorDetails(int dogovorID)
        {
            var dogovorInDb = _context.Dogovors.Include(p => p.Organization).SingleOrDefault(p => p.ID == dogovorID);
            OrganizationDogovorVM viewModel = new OrganizationDogovorVM
            {
                DogovorId = dogovorInDb.ID,
                OrderHeader = dogovorInDb.OrderHeader,
                DogovorSum = dogovorInDb.DogovorSum,
                Currency = dogovorInDb.Currency,
                OrganizationName = dogovorInDb.Organization.Name                
            };

            return View("Details", viewModel);
        }

     
    }
}
