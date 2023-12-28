using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.DAL;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{

    public class OrganizationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IOrganizationRepository organizationRepository;
        private PersonOrganizationVM organizationVM;

        public OrganizationController()
        {
            _context = new ApplicationDbContext();
            this.organizationRepository = new OrganizationRepository(new ApplicationDbContext());
            this.organizationVM = new PersonOrganizationVM();
        }

        public IActionResult Index ()
        {
            var organizationsInDb = organizationRepository.GetAllOrganization();
            var organizations = organizationVM.GetPersonOrganizationList(organizationsInDb);

            return View(organizations);
        }
        
        public IActionResult Create()
        {
            return View("PersonOrgForm", organizationVM);
        }

        public IActionResult Edit(int id)
        {
            var person = _context.Employees.Include(p => p.Organization).SingleOrDefault(p => p.ID == id);

            organizationVM = new PersonOrganizationVM
            {
                OrganizationID = person.Organization.ID,
                OrganizationName = person.Organization.Name,
                CustFirstName = person.FirstName,
                CustLastName = person.LastName,
                CustPhoneNumber = person.PhoneNumber,
                CustEmail = person.Email,
                PersonID = person.ID,
            };

            return View("PersonEditForm", organizationVM);
        }

        public IActionResult Delete(int? orgId, int? personId)
        {
            if(orgId != null && orgId != 0)
            {
                var organizationInDb = _context.Organizations.Include(p => p.Person).Include(p => p.Dogovors).SingleOrDefault(p => p.ID == orgId);
                _context.Organizations.Remove(organizationInDb);
            }
            
            if  (personId != null && personId != 0)
            {
                var personInDb = _context.Employees.SingleOrDefault(p => p.ID == personId);
                _context.Employees.Remove(personInDb);
            }


            
            _context.SaveChanges();

            return RedirectToAction("Index", "Organization");
        }

        public IActionResult Save(PersonOrganizationVM formData)
        {
            if (!ModelState.IsValid)
            {
                organizationVM = formData;
                return View("PersonOrgForm", organizationVM);
            }


            if (formData.OrganizationID == 0)
            {
                var organization = organizationVM.AddNewOrganization(formData);
                organizationRepository.InsertOrganization(organization);
                organizationRepository.Save();
            }
            else
            {
                var organizationInDb = _context.Organizations.Include(p => p.Person).Single(p => p.ID == formData.OrganizationID);
                var person = organizationInDb.Person.SingleOrDefault(p => p.ID == formData.PersonID);

                organizationInDb.Name = formData.OrganizationName;
                person.FirstName = formData.CustFirstName;
                person.LastName = formData.CustLastName;
                person.PhoneNumber = formData.CustPhoneNumber;
                person.Email = formData.CustEmail;
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



