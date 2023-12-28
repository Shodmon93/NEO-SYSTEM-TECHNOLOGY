using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;
using System.Collections.Immutable;



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
            List<OrganizationDogovorVM> list = new List<OrganizationDogovorVM>();

            foreach (var d in dogovorList)
            {
                if (d.IsOneTimeDogovor)
                {
                    list.Add(new OrganizationDogovorVM
                    {
                        OrderHeader = d.OrderHeader,
                        OrganizationName = d.Organization.Name,
                        OrganizationId = d.Organization.ID,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        IsVatIncluded = d.IsVatIncluded,
                        IsOneTimeDogovor = d.IsOneTimeDogovor,
                        Currency = d.Currency,
                        DogovorSum = d.DogovorSum,
                        DogovorId = d.ID

                    });
                }
            }

            return View(list);
        }

        public IActionResult AddNewDogovor(int id, bool isOneTimeDogovor)
        {
            var organization = _context.Organizations.SingleOrDefault(p => p.ID == id);
            if (isOneTimeDogovor == true)
            {
               
                var viewModel = new OrganizationDogovorVM()
                {
                    OrganizationId = organization.ID,
                    OrganizationName = organization.Name,
                    IsOneTimeDogovor = isOneTimeDogovor
                };

                return View("DogovorForm", viewModel);

            }
            else
            {
                var rmViewModel = new RmDogovorOrganization()
                {
                    OrganizationId = organization.ID,
                    OrganizationName = organization.Name,
                    IsOneTimeDogovor = isOneTimeDogovor
                };
                return View("RmDogovorForm", rmViewModel);
            }

           
        }
        public IActionResult Save(OrganizationDogovorVM viewModel) 
        {
            var organizationInDb = _context.Organizations.Single(p => p.ID == viewModel.OrganizationId);

            if (!ModelState.IsValid)
            {
                var vm = new OrganizationDogovorVM
                {
                    Organization = organizationInDb,
                    Dogovor = viewModel.Dogovor
                };
                return View("DogovorForm", vm);
            }
            var priceWithTax = viewModel.DogovorSum + (viewModel.DogovorSum * ( TAX / 100));

            if (viewModel.DogovorId == 0)
            {
                if (viewModel.IsVatIncluded)
                {
                    viewModel.DogovorSum = priceWithTax;
                }

                Dogovor dogovor = new Dogovor
                {
                    OrderHeader = viewModel.OrderHeader,
                    DogovorSum = viewModel.DogovorSum,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate,
                    Currency = viewModel.Currency,
                    IsVatIncluded = viewModel.IsVatIncluded,
                    Organization = organizationInDb,
                    IsOneTimeDogovor = viewModel.IsOneTimeDogovor,
                };

                _context.Dogovors.Add(dogovor);
            }
            else
            {
                var contractInDb = _context.Dogovors.Single(p => p.ID == viewModel.DogovorId);
                contractInDb.OrderHeader = viewModel.OrderHeader;
                contractInDb.DogovorSum =  viewModel.DogovorSum;
                contractInDb.StartDate = viewModel.StartDate;
                contractInDb.EndDate = viewModel.EndDate;
                contractInDb.Currency = viewModel.Currency;
                contractInDb.IsVatIncluded = viewModel.IsVatIncluded;
            }
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Dogovor");
        }

        public IActionResult EditDogovor(int id)
        {
            var dogovorInDb = _context.Dogovors.Include(p => p.Organization).Single(p => p.ID == id);
            var viewModel = new OrganizationDogovorVM
            {
                DogovorId = dogovorInDb.ID,
                OrganizationId = dogovorInDb.Organization.ID,
                OrganizationName = dogovorInDb.Organization.Name,
                OrderHeader = dogovorInDb.OrderHeader,
                StartDate = dogovorInDb.StartDate,
                EndDate = dogovorInDb.EndDate,
                Currency = dogovorInDb.Currency,
                DogovorSum = dogovorInDb.DogovorSum,
                IsVatIncluded = dogovorInDb.IsVatIncluded,

            };
            return View("DogovorForm", viewModel);
            
        }

        public IActionResult RmDogovorIndex()
        {
            var dogovorInDb = _context.Dogovors.Include(p => p.Zakaz).Include(p => p.Organization).ToList();
            RmDogovorOrganization rmDogovor = new RmDogovorOrganization();
            List<RmDogovorOrganization>  list = new List<RmDogovorOrganization>();
            foreach (var d in dogovorInDb)
            {
                if (d.IsOneTimeDogovor == false)
                {
                    list.Add(new RmDogovorOrganization
                    {
                        OrganizationName = d.Organization.Name,
                        Orders = d.Zakaz,
                        OrderHeader = d.OrderHeader,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        Currency = d.Currency,
                        DogovorSum = d.DogovorSum,
                        IsVatIncluded = d.IsVatIncluded
                    });
                }

            }


            return View(list);
        }

        public IActionResult RmDogovorSave(RmDogovorOrganization rmViewModel)
        {
            var organizationInDb = _context.Organizations.Single(p => p.ID == rmViewModel.OrganizationId);
            if (!ModelState.IsValid)
            {
                var vm = new RmDogovorOrganization
                {
                    OrganizationName = organizationInDb.Name,
                    OrderHeader = rmViewModel.OrderHeader,
                };
                return View("DogovorForm", vm);
            }
            var priceWithTax = rmViewModel.DogovorSum + (rmViewModel.DogovorSum * (TAX / 100));
            if (rmViewModel.DogovorId == 0)
            {
                if (rmViewModel.IsVatIncluded)
                {
                    rmViewModel.DogovorSum = priceWithTax;
                }

                Dogovor dogovor = new Dogovor
                {
                    OrderHeader = rmViewModel.OrderHeader,
                    DogovorSum = rmViewModel.DogovorSum,
                    StartDate = rmViewModel.StartDate,
                    EndDate = rmViewModel.EndDate,
                    Currency = rmViewModel.Currency,
                    IsVatIncluded = rmViewModel.IsVatIncluded,
                    Organization = organizationInDb,
                    IsOneTimeDogovor = rmViewModel.IsOneTimeDogovor,
                    Zakaz = new List<Zakaz>
                    {
                        new Zakaz
                        {
                            OrderNumber = rmViewModel.OrderNumber,
                        }
                    }
                };
                _context.Dogovors.Add(dogovor);
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
                OrganizationName = dogovorInDb.Organization.Name,
                StartDate = dogovorInDb.StartDate,
                EndDate = dogovorInDb.EndDate,
            };

            return View("Details", viewModel);
        }

     
    }
}
