using LBSJourney.Models;
using LBSJourney.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LBSJourney.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppSecurity _appSecurity;
        private LbsDB _lbsDB;
        private AxiosBaseURL _axiosBaseURL;

        public HomeController(ILogger<HomeController> logger, AppSecurity appSecurity, LbsDB lbsDB, AxiosBaseURL axiosBaseURL)
        {
            _logger = logger;
            _appSecurity = appSecurity;
            _lbsDB = lbsDB;
            _axiosBaseURL = axiosBaseURL;
        }

        public IActionResult Register(Users user)
        {
            if(this.Request.Method.Equals("POST"))
            {
                try
                {
                    String hashUserName = _appSecurity.hash512(user.UserName);
                    String hashPassword = _appSecurity.hash512(user.Password);
                    user.UserName = hashUserName;
                    user.Password = hashPassword;
                    user.CreateTime = DateTime.Now;
                    // Console.WriteLine($"帳號: {hashUserName}");
                    // Console.WriteLine($"密碼: {hashPassword}");
                    // Console.WriteLine($"Email: {user.Email}");
                    _lbsDB.Users.Add(user);
                    Int32 rows = _lbsDB.SaveChanges();
                    if (rows > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Register");
                    }
                }catch(DbUpdateException ex)
                {
                    Console.WriteLine("資料庫更新失敗");
                }
            }
            return View();
        }

        public IActionResult Index(Users user)
        {
            if (this.Request.Method.Equals("POST"))
            {
                // Console.WriteLine($"{user.UserName}\n{user.Password}");
                String originalUserName = user.UserName;
                String hashUserName = _appSecurity.hash512(user.UserName);
                String hashPassword = _appSecurity.hash512(user.Password);
                var result = _lbsDB.Users.Where(u => u.UserName.Equals(hashUserName) && u.Password.Equals(hashPassword)).FirstOrDefault();
                // Console.WriteLine(result);
                if (result != null)
                {
                    HttpContext context = this.HttpContext;
                    ISession session = context.Session;
                    session.SetString(".UserName", originalUserName);
                    return RedirectToAction("AllUsersMap", "Map");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            String baseURL = _axiosBaseURL.url;
            ViewData["baseURL"] = baseURL;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}