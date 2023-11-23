using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;
using System.Diagnostics.Contracts;
using Contract = NEO_SYSTEM_TECHNOLOGY.Entity.Contract;



namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class ContractController : Controller
    {
      private const decimal TAX = 15;

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
            var organizationInDb = _context.Organizations.Single(p => p.ID == viewModel.Organization.ID);

            if (!ModelState.IsValid)
            {
                var vm = new OrganizationContractVM
                {
                    Organization = organizationInDb,
                    Contract = viewModel.Contract
                };
                return View("ContractForm", vm);
            }
            var priceWithTax = viewModel.Contract.ContractSum + (viewModel.Contract.ContractSum * ( TAX / 100));

            if (viewModel.Contract.ID == 0)
            {
                if (viewModel.Contract.IsVatIncluded)
                {
                    viewModel.Contract.ContractSum = priceWithTax;
                }

                Contract contract = new Contract
                {
                    OrderNumber = viewModel.Contract.OrderNumber,
                    ContractSum = viewModel.Contract.ContractSum,
                    StartDate = viewModel.Contract.StartDate,
                    EndDate = viewModel.Contract.EndDate,
                    Currency = viewModel.Contract.Currency,
                    IsVatIncluded = viewModel.Contract.IsVatIncluded,
                    Organization = organizationInDb
                };

                _context.Contracts.Add(contract);
            }
            else
            {
                var contractInDb = _context.Contracts.Single(p => p.ID == viewModel.Contract.ID);
                contractInDb.OrderNumber = viewModel.Contract.OrderNumber;
                contractInDb.ContractSum =  viewModel.Contract.ContractSum;
                contractInDb.StartDate = viewModel.Contract.StartDate;
                contractInDb.EndDate = viewModel.Contract.EndDate;
                contractInDb.Currency = viewModel.Contract.Currency;
                contractInDb.IsVatIncluded = viewModel.Contract.IsVatIncluded;
            }
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Contract");
        }

        public IActionResult Index()
        {
            var contractList = _context.Contracts.Include(p => p.Organization).ToList();
                     

            return View(contractList);
        }
    }
}
