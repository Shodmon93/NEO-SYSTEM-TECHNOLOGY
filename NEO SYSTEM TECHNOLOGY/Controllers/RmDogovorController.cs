using Microsoft.AspNetCore.Mvc;
using NEO_SYSTEM_TECHNOLOGY.DAL;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;
using System.Linq.Expressions;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class RmDogovorController : Controller
    {

        private readonly UnitOfWork unitOfWork;
        private readonly RmDogovorOrganizationVM rmDogovorVM;       


        public RmDogovorController()
        {
            this.unitOfWork = new UnitOfWork();
            this.rmDogovorVM = new RmDogovorOrganizationVM();
        }

        public IActionResult Index()
        {
            var rmDogovors = unitOfWork.DogovorRepository.GetAll(p => p.IsOneTimeDogovor == false, include2: p => p.Organization );
            var result = rmDogovorVM.GetAllRmDogovors(rmDogovors);

            IEnumerable<string> listOfDirectories = Directory.EnumerateDirectories("stores");

            //IEnumerable<string> files = Directory.EnumerateFiles("C:\\Users\\Shodmon.Ahmadov\\source\\repos\\New folder\\NEO SYSTEM TECHNOLOGY\\stores\\");

            foreach (var dir in listOfDirectories)
            {
                Console.WriteLine(dir);
                ViewBag.Directory = dir;

                IEnumerable<string> files = Directory.EnumerateFiles(dir/*, "*.pdf"*/);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }





            return View(result);
        }

        public IActionResult Details(int dogovorId)
        {
            var rmDogovor = unitOfWork.DogovorRepository.GetByID(p => p.ID == dogovorId, new Expression<Func<Dogovor, object>>[] { p => p.Zakaz, p => p.Organization });
            var viewModel = rmDogovorVM.GetDogovor(rmDogovor);

            return View(viewModel);
        }

        public IActionResult AddNewRmDogovor(int organizationId, bool isOneTimeDogovor)
        {
            var organization = unitOfWork.OrganizationRepository.GetByID(organizationId);
            if (organization == null)
            {
                return NotFound();
            }
            var rmDogovor = rmDogovorVM.GetOrganization(organization, isOneTimeDogovor);
            return View("RmDogovorForm", rmDogovor);
            
        }


        public IActionResult Save(RmDogovorOrganizationVM formData) 
        {
            if (!ModelState.IsValid)
            {
              return  View("RmDogovorForm", formData);
            }
            
            if (formData.DogovorId == 0)
            {
                var organization = unitOfWork.OrganizationRepository.GetByID(formData.OrganizationId);
                var dogovor = rmDogovorVM.SaveRmDogovor(formData, organization);
                unitOfWork.DogovorRepository.Insert(dogovor);
            }

            unitOfWork.Save();

            return RedirectToAction("Index","RmDogovor");
        }

        public IActionResult AddNewZakaz(int dogovorId)
        {
            var rmDogovor = unitOfWork.DogovorRepository.GetByID(p => p.ID == dogovorId, new Expression<Func<Dogovor, object>>[] { p => p.Zakaz, p => p.Organization });
            var viewModel = rmDogovorVM.AddNewZakaz(rmDogovor);

            return View("ZakazForm", viewModel);
        }

        public IActionResult SaveNewZakaz(RmDogovorOrganizationVM formData)
        {
            if (!ModelState.IsValid)
            {
                return View("ZakazForm", formData);
            }
            var newOrder = rmDogovorVM.GetNewZakaz(formData);
            var rmDogovor = unitOfWork.DogovorRepository.GetByID(p => p.ID == formData.DogovorId);
            rmDogovor.Zakaz.Add(newOrder);
            
            unitOfWork.DogovorRepository.Update(rmDogovor);
            unitOfWork.Save();

            return RedirectToAction("Index", "RmDogovor", formData);

        }
    }
}
