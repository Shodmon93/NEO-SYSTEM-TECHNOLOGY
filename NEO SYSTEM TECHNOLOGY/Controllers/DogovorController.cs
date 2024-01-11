using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Controllers.GeneralController;
using NEO_SYSTEM_TECHNOLOGY.DAL;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;



namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class DogovorController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork unitOfWork;
        private readonly OrganizationDogovorVM _dogovorVM;
        private readonly RmDogovorOrganizationVM _rmDogovorVM;

        public DogovorController()
        {
            _context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork();
            _dogovorVM = new OrganizationDogovorVM();
            _rmDogovorVM = new RmDogovorOrganizationVM();
        }

        public IActionResult Index()
        {
            var dogovorsInDb = unitOfWork.DogovorRepository.GetAll(includeProperties: p => p.Organization);
            var dogovors = _dogovorVM.GetAllDogovors(dogovorsInDb);

            return View(dogovors);
        }

        public IActionResult AddNewDogovor(int organizationId, bool isOneTimeDogovor)
        {
            var organization = unitOfWork.OrganizationRepository.GetByID(organizationId);
            if (organization == null)
            {
                return NotFound();
            }
            var dogovor = _dogovorVM.GetOrganization(organization, isOneTimeDogovor);
            var rmDogovor = _rmDogovorVM.GetOrganization(organization, isOneTimeDogovor);

            return HandleDogovor(isOneTimeDogovor,
                () => View("DogovorForm", dogovor),
                () => RedirectToAction("AddNewRmDogovor", "RmDogovor", rmDogovor));

        }
        public IActionResult Save(OrganizationDogovorVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("DogovorForm", viewModel);
            }

            var organizationInDb = unitOfWork.OrganizationRepository.GetByID(viewModel.OrganizationId);
            if (organizationInDb == null)
            {
                return NotFound();
            }
            if (viewModel.DogovorId == 0)
            {
                var dogovorToInsert = _dogovorVM.GetDogovorToInsert(viewModel, organizationInDb);
                unitOfWork.DogovorRepository.Insert(dogovorToInsert);

            }
            else
            {
                var dogovorInDb = unitOfWork.DogovorRepository.GetByID(viewModel.DogovorId);
                var dogovorToUpdate = _dogovorVM.GetDogovorToUpdate(dogovorInDb, viewModel);
                unitOfWork.DogovorRepository.Update(dogovorToUpdate);
            }
            unitOfWork.Save();

            return RedirectToAction("Index", "Dogovor");
        }

        public IActionResult EditDogovor(int dogovorID)
        {
            var dogovorInDb = unitOfWork.DogovorRepository.GetByID(p => p.ID == dogovorID,  includeProperties: p => p.Organization);
            var dogovor = _dogovorVM.EditDogovor(dogovorInDb);

            return View("DogovorForm", dogovor);

        }




        // To be removed to another namespace for RmDogovorController

        public IActionResult RmDogovorIndex()
        {
            var dogovorInDb = _context.Dogovors.Include(p => p.Zakaz).Include(p => p.Organization).ToList();
            RmDogovorOrganizationVM rmDogovor = new RmDogovorOrganizationVM();
            List<RmDogovorOrganizationVM> list = new List<RmDogovorOrganizationVM>();
            foreach (var d in dogovorInDb)
            {
                if (d.IsOneTimeDogovor == false)
                {
                    list.Add(new RmDogovorOrganizationVM
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

        public IActionResult RmDogovorSave(RmDogovorOrganizationVM rmViewModel)
        {
            var organizationInDb = _context.Organizations.Single(p => p.ID == rmViewModel.OrganizationId);
            if (!ModelState.IsValid)
            {
                var vm = new RmDogovorOrganizationVM
                {
                    OrganizationName = organizationInDb.Name,
                    OrderHeader = rmViewModel.OrderHeader,
                };
                return View("DogovorForm", vm);
            }
 //           var priceWithTax = rmViewModel.DogovorSum + (rmViewModel.DogovorSum * (TAX / 100));
            if (rmViewModel.DogovorId == 0)
            {
                //if (rmViewModel.IsVatIncluded)
                //{
                //    rmViewModel.DogovorSum = priceWithTax;
                //}

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
                FilePath = Convert.ToBase64String(dogovorInDb.Content)
            };

            return View("Details", viewModel);
        }


    }
}
