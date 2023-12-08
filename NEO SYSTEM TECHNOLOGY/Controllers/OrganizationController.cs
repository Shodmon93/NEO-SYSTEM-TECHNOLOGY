using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
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
            PersonOrganizationVM result = new PersonOrganizationVM();
            var organizationList = result.GetPersonOrganizationList(organization);

            return View(organizationList);
        }

        public IActionResult Create()
        {
            var viewModel = new PersonOrganizationVM();

            return View("PersonOrgForm", viewModel);
        }       

        public IActionResult Save(PersonOrganizationVM result)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = result;
                return View("PersonOrgForm", viewModel);
            }

            if (result.OrganizationID == 0)
            {
                PersonOrganizationVM viewModel = new PersonOrganizationVM();
                var organization = viewModel.AddNewOrganization(result);
                _context.Organizations.Add(organization);
            }
            else
            {
                var organizationInDb = _context.Organizations.Include(p => p.Person).Single(p => p.ID == result.OrganizationID);
                organizationInDb.Name = result.OrganizationName;
                organizationInDb.Person = new List<Person>()
                {
                    new Person()
                    {
                        FirstName = result.CustFirstName,
                        LastName = result.CustLastName,
                        PhoneNumber = result.CustPhoneNumber,
                        Email = result.CustEmail,
                    }
                };
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Organization");

        }

        public IActionResult Details(int organizationId)
        {
            var organization = _context.Organizations.Include(p => p.Person).SingleOrDefault(p => p.ID == organizationId);
            PersonOrganizationVM result = new PersonOrganizationVM();
            var organizationDetails = result.GetPersonOrganizationByID(organization);

            return View(organizationDetails);
        }

    }
}



