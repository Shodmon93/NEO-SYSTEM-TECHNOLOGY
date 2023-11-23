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
        private readonly ApplicationDbContext _context;

        public OrganizationController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            var organization = _context.Organizations.Include(p => p.Person).ToList();

            return View(organization);
        }

        public IActionResult Details(int id) 
        {
            var organization = _context.Organizations.Include(p => p.Person).SingleOrDefault(p => p.ID == id);

            return View(organization);

        }

        public IActionResult Save(PersonOrganizationVM vm)
        {
            if (!ModelState.IsValid) 
            {
                var viewModel = new PersonOrganizationVM
                {
                    Organization = vm.Organization,
                    Person = vm.Person
                };
                return View("PersonOrgForm", viewModel);
            }

            if (vm.Organization.ID == 0) 
            {
                Organization organization = new()
                {
                    Name = vm.Organization.Name,
                    Person = new List<Person>
                    {
                        new Person
                        {
                            FirstName = vm.Person.FirstName,
                            LastName = vm.Person.LastName,
                            PhoneNumber = vm.Person.PhoneNumber,
                            Email = vm.Person.Email,
                        }
                    }
                };

                _context.Organizations.Add(organization);
            }
            else
            {
                var organizationInDb = _context.Organizations.Single(p => p.ID == vm.Organization.ID);
                organizationInDb.Name = vm.Organization.Name;
                organizationInDb.Person = new List<Person>()
                {
                    new Person()
                    {
                        FirstName = vm.Person.FirstName,
                        LastName = vm.Person.LastName,
                        PhoneNumber = vm.Person.PhoneNumber,
                        Email = vm.Person.Email,
                    }
                };
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Organization");

        }

        public IActionResult Create()
        {
            var viewModel = new PersonOrganizationVM
            {
                Organization = new Organization(),
                Person = new Person()
            };

            return View("PersonOrgForm", viewModel);
        }

    }
}



