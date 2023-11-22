using Microsoft.AspNetCore.Mvc;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;
using System.Diagnostics.Contracts;
using Contract = NEO_SYSTEM_TECHNOLOGY.Entity.Contract;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class ContractController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult AddNewContract(int id)
        {
            var organization = _context.Organizations.SingleOrDefault(p => p.ID == id);
            var viewModel = new OrganizationContractVM()
            {
                Organization = organization
            };

            return View("ContractForm", viewModel);
        }
        public IActionResult Save(OrganizationContractVM viewModel) 
        {
            var organizationInDb = _context.Organizations.SingleOrDefault(p => p.ID == viewModel.Organization.ID);

            if  (organizationInDb == null)
            {
                return NotFound();

            }

            var contract = new Contract()
            {
                Organization = organizationInDb,
                OrderNumber = viewModel.Contract.OrderNumber,
                ContractSum = viewModel.Contract.ContractSum,
                StartDate = viewModel.Contract.StartDate,
                EndDate = viewModel.Contract.EndDate,

            };
            _context.Contracts.Add(contract);
            _context.SaveChanges();

            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
