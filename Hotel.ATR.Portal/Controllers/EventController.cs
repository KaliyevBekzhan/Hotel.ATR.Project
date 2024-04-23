using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR.Portal.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Event()
        {
            return View();
        }
    }
}
