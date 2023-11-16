using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class OrganizationController : Controller
    {



        private ApplicationDbContext _context;

        public OrganizationController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {

            return View("Index");
        }

        public IActionResult Save(RegisterFormViewModel viewModel)
        {

            var organization = new Organization();

            

            _context.Add(viewModel.Organization);
            _context.Add(viewModel.Person);
           // _context.Add(viewModel.Contract);
           // _context.Add(viewModel.Invoice);




            //_context.Add(organization);
            //_context.Add(person);
            //_context.Add(contract);
            //_context.Add(invoice);
            _context.SaveChanges();


            return View("Index");
        }

        public IActionResult Create(string contractType)
        {
            var viewModel = new RegisterFormViewModel()
            {
                Organization = new Organization(),
                Person = new Person(),
                Contract = new Contract(),
                Receipt = new Receipt(),
                Invoice = new Invoice(),
                Enactment = new Enactment()
            };

           
             return View("RegisterForm", viewModel);
        }

        public IActionResult CaseTest()
        {
            return View("TestView"); 
        }
    }
}



