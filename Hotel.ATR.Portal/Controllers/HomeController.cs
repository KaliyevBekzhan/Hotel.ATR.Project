using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace Hotel.ATR.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor _httpContext;
        private IConfiguration _config;
        private IOptions<APIEndpoint> _settings;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContext,
            IConfiguration config, IOptions<APIEndpoint> settings)
        {
            _logger = logger;
            _httpContext = httpContext;
            _config = config;
            _settings = settings;
        }

        User user = new User("ok@ok.kz");

        [Authorize]
        public IActionResult Index(string culture)
        {
            var data0 = _settings.Value.Name;
            var data4 = _config.GetSection("APIEndpoint").GetSection("Name").Value;
            var data5 = _config.GetSection("APIEndpoint:Name").Value;
            var data6 = _config.GetSection("APIEndpoint").GetValue<bool>("IsSecured");


            if (!string.IsNullOrWhiteSpace(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);
            }



            HttpContext.Session.SetString("iin", "030531550379");

            var data3 = HttpContext.Session.GetString("iin");

            var data2 = _httpContext.HttpContext.Request.Cookies["iin"];

            CookieOptions options = new CookieOptions();

            //options.Expires = DateTime.Now.AddSeconds(100);
            //Response.Cookies.Append("iin", "030531550379", options);

            //var value = Request.Cookies["iin"];

            _logger.LogError("У пользователя {email} возникла ошибка {errorMessage}, {action}, {controller}", user.email, "Ошибка пользователя", "Index", "Home");

            Stopwatch sw = new Stopwatch();

            _logger.LogInformation("Обращаемся к сервису");

            sw.Start();
            //Thread.Sleep(1000);
            ///TODO
            sw.Stop();
            var data = sw.ElapsedMilliseconds;

            _logger.LogInformation("Сервис отработал за {ElapsedMiliseconds}", sw.ElapsedMilliseconds);


            _logger.LogError("Logging error");
            _logger.LogWarning("Logging warning");
            _logger.LogDebug("Logging debug");

            return View();
        }

        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password, string ReturnUrl)
        {
            if (login == "admin" && password == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login)
                };
                var ClaimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity));

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectPermanent(ReturnUrl);
                }
                /*
                Task.Delay(100);

                return Redirect(ReturnUrl == null ? "/Index" : ReturnUrl);
                */
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetUser()
        {
            User user = new User("bekzhan@gmail.com");
            user.name = "Bekzhan.Kaliyev";
            
            return Json(user);
        }
    }
}