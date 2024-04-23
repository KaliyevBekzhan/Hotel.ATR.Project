using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR.Portal.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Location()
        {
            return View();
        }
    }
}
