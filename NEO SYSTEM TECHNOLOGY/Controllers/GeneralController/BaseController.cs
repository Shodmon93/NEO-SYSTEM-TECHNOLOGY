using Microsoft.AspNetCore.Mvc;
using NEO_SYSTEM_TECHNOLOGY.DAL;

namespace NEO_SYSTEM_TECHNOLOGY.Controllers.GeneralController
{
    public class BaseController : Controller
    {        
        public BaseController()
        {
            
        }

        public IActionResult HandleDogovor(bool isOneTimeDogovor,
            Func<IActionResult> oneTimeDogovorAction,
            Func<IActionResult> rmDogovorAction)
        {
            return isOneTimeDogovor ? oneTimeDogovorAction() : rmDogovorAction();
        }
    }
}
