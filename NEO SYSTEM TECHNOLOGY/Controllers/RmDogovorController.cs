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
            Expression<Func<Dogovor, bool>> filter = p => p.IsOneTimeDogovor == false;
            var rmDogovors = unitOfWork.DogovorRepository.GetAll(new Expression<Func<Dogovor, object>>[] {p => p.Organization, p => p.Zakaz}, p => p.IsOneTimeDogovor == false);
            var result = rmDogovorVM.GetAllRmDogovors(rmDogovors);
            return View(result);
        }

        public IActionResult Save(RmDogovorOrganizationVM formData) 
        {
            var organization = unitOfWork.OrganizationRepository.GetByID(formData.OrganizationId);
            var dogovor = rmDogovorVM.SaveRmDogovor(formData, organization);
            unitOfWork.DogovorRepository.Insert(dogovor);
            unitOfWork.Save();

            return RedirectToAction("Index","RmDogovor");
        }

        public IActionResult Details(int dogovorId)
        {
            var dogovor = unitOfWork.DogovorRepository.GetByID(p => p.ID == dogovorId, p => p.Zakaz);



            return View(dogovor);
        }
    }
}
