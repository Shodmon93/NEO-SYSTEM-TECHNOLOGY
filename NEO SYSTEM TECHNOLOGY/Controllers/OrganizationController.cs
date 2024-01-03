using Microsoft.AspNetCore.Mvc;
using NEO_SYSTEM_TECHNOLOGY.DAL;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{

    public class OrganizationController : Controller
    {
        
        private readonly UnitOfWork unitOfWork;
        private PersonOrganizationVM organizationVM;

        public OrganizationController()
        {
            unitOfWork = new UnitOfWork();
            this.organizationVM = new PersonOrganizationVM();
        }

        public IActionResult Index()
        {
            var organizations = organizationVM.GetPersonOrganizationList(
                unitOfWork.OrganizationRepository.GetAll(includeProperties: p => p.Person));

            return View(organizations);
        }

        public IActionResult Create()
        {
            return View("PersonOrgForm", organizationVM);
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
                unitOfWork.OrganizationRepository.Insert(organization);               
            }
            else
            {
                var organizationInDb = unitOfWork.OrganizationRepository.GetByID(formData.OrganizationID);
                var organizationToUpdate = organizationVM.EditOrganization(organizationInDb, formData);
                unitOfWork.OrganizationRepository.Update(organizationToUpdate);
            }
            unitOfWork.Save();

            return RedirectToAction("Index", "Organization");

        }

        public IActionResult Delete(int orgId)
        {
            if (orgId != 0)
            {
                var organizationInDb = unitOfWork.OrganizationRepository.GetByID(p => p.ID == orgId, p => p.Person);
                unitOfWork.OrganizationRepository.Delete(organizationInDb);
                unitOfWork.Save();
            }
            return RedirectToAction("Index", "Organization");
        }

        public IActionResult Details(int organizationId)
        {
            var organization = unitOfWork.OrganizationRepository.GetByID(p => p.ID == organizationId, p => p.Person);
            var organizationDetails = organizationVM.GetPersonOrganizationByID(organization);

            return View(organizationDetails);
        }
    }
}



