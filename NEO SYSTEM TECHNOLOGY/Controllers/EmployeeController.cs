using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.DAL;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class EmployeeController : Controller
    {
    
        private PersonOrganizationVM viewModel;
        private readonly UnitOfWork unitOfWork;

        public EmployeeController()
        {
            viewModel = new PersonOrganizationVM();
            unitOfWork = new UnitOfWork();
        }

        public IActionResult AddNewEmployee(int organizationId)
        {
            var organizationInDb = unitOfWork.OrganizationRepository.GetByID(organizationId);
            viewModel = new PersonOrganizationVM
            {
                OrganizationID = organizationInDb.ID,
                OrganizationName = organizationInDb.Name,
            };

            return View("EmployeeForm", viewModel);
        }

        public IActionResult EditEmployee(int employeeId)
        {
            var employeeInDb = unitOfWork.EmployeeRepository.GetByID(employeeId);
            var organizationInDb = unitOfWork.OrganizationRepository.GetByID(employeeInDb.OrganizationID);
            viewModel.EditEmployee(employeeInDb, organizationInDb);

            return View("EmployeeForm", viewModel);
        }

        public IActionResult Save(PersonOrganizationVM formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    viewModel = formData;
                    return View("EmployeeForm", viewModel);
                }
                if (formData.PersonID == 0)
                {
                    var organizationInDb = unitOfWork.OrganizationRepository.GetByID(formData.OrganizationID);
                    var employee = viewModel.AddNewEmployee(formData, organizationInDb);
                    unitOfWork.EmployeeRepository.Insert(employee);
                }
                else
                {
                    var employeeInDb = unitOfWork.EmployeeRepository.GetByID(formData.PersonID);
                    var employee = viewModel.EditEmployee(formData, employeeInDb);
                    unitOfWork.EmployeeRepository.Update(employee);
                }
                unitOfWork.Save();

            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Не удается сохранить изменения. Повторите попытку, и если проблема не устранится, обратитесь к системному администратору.");
            }
            return RedirectToAction("Details", "Organization", formData);
        }

        public IActionResult Delete(int employeeID)
        {
            if (employeeID != 0)
            {
                var employeeInDb = unitOfWork.EmployeeRepository.GetByID(employeeID);
                var organization = unitOfWork.OrganizationRepository.GetByID(employeeInDb.OrganizationID);

                viewModel = new PersonOrganizationVM
                {
                    OrganizationName = organization.Name,
                    OrganizationID = organization.ID

                };
                unitOfWork.EmployeeRepository.Delete(employeeID);
                unitOfWork.Save();

                return RedirectToAction("Details", "Organization", viewModel);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
