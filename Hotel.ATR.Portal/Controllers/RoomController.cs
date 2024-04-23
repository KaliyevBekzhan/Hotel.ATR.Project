using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hotel.ATR.Portal.Controllers
{
    public class RoomController : Controller
    {
        private IWebHostEnvironment webHost;
        private readonly ILogger<RoomController> _logger;
        public RoomController(IWebHostEnvironment webHost, ILogger<RoomController> _logger)
        {
            this.webHost = webHost;
            this._logger = _logger;
        }
        /*
        var user = new User() { email = "ok@ok.kz", name = "bekzhan" };
            
        //Неправильные способы

        ViewBag.User = user;
        ViewData["user"] = user;
        TempData["user"] = user;
        */
        public async Task<IActionResult> RoomAsync()
        {
            RoomData data = new RoomData();
            List<Room> rooms = new List<Room>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5198/api/room"))
                {
                    string apiRequest = await response.Content.ReadAsStringAsync();

                    rooms = JsonConvert.DeserializeObject<List<Room>>(apiRequest);
                }
            }

            data.Rooms = rooms;
            data.Clients = null;

            _logger.LogInformation("Logging information");
            _logger.LogError("Logging error");
            _logger.LogWarning("Logging warning");
            _logger.LogDebug("Logging debug");
            return View(data); 
        }
        public IActionResult Roomdetails()
        {
            return View();
        }
        public IActionResult Roomlist()
        {
            return View();
        }

        //string name
        //User user
        [HttpPost]
        public IActionResult SubscribeNewsletter(IFormFile userfile)
        {
            var data = Request.Form;

            string path = Path.Combine(webHost.WebRootPath, userfile.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                userfile.CopyTo(stream);
            }

            //return View("Index");
            
            //return View("~/Views/Home/Index.cshtml");
            return RedirectToAction("Index");
        }
    }
}
