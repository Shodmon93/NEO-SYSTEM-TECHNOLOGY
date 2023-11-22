using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;


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

        public IActionResult AddNewContract(int id) 
        {
            var organization = _context.Organizations.SingleOrDefault(p => p.ID == id);

            if (organization == null)
            {
                return NotFound();
            }





            return View("PersonOrgForm");
        }

        public IActionResult Create()
        {
            return View("PersonOrgForm");
        }


    }
}



