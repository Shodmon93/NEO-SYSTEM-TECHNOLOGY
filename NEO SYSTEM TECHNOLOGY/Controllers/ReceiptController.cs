using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReceiptController(IHostingEnvironment hostingEnvironment)
        {
            _context = new ApplicationDbContext();
            _hostingEnvironment = hostingEnvironment;            
        }

        public IActionResult Index()
        {
            var receipList = _context.Receipts.Include(p => p.Dogovor).ThenInclude(p => p.Organization).ToList();
          

            return View(receipList);
        }

        public IActionResult AddNewReceipt(int dogovorId)
        {
            var dogovorInDb = _context.Dogovors.Include(p => p.Receipt).Include(p => p.Organization).SingleOrDefault(p => p.ID == dogovorId);
            var viewModel = new DogovorReceiptVM
            {
                Order = dogovorInDb.OrderHeader,
                OrgName = dogovorInDb.Organization.Name,
                DogovorId = dogovorInDb.ID
            };
            return View("ReceiptForm", viewModel);
        }

        public IActionResult Save(DogovorReceiptVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                var vm = new DogovorReceiptVM
                {
                    Order = viewModel.Order,
                    OrgName = viewModel.OrgName
                };
                return View("ReceiptForm", vm);
            }

            string fileName = null;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "pdfFile");
            fileName = Guid.NewGuid().ToString() + "_" + viewModel.FormFile.FileName;
            string filePath = Path.Combine(uploadFolder, fileName);
            viewModel.FormFile.CopyTo(new FileStream(filePath, FileMode.Create));



            Receipt receipt = new Receipt
            {
                AccountNumber = viewModel.AccountNumber ?? default,
                AmountFaceValue = viewModel.AmountFaceValue ?? default,
                IsVatIncluded = viewModel.IsVatIncluded,
                Currency = viewModel.Currency,
                PaymentSum = viewModel.PaymentSum ?? default,
                DateFaceValue = viewModel.DateFaceValue ?? default,
                PaymentDate = viewModel.PaymentDate ?? default,
                DogovorId = viewModel.DogovorId,
                Content = fileName
            };

            _context.Add(receipt);
            _context.SaveChanges();




            return RedirectToAction("Index");
        }
    }
}
