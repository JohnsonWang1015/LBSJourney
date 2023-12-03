using LBSJourney.Models;
using LBSJourney.Models.Dao;
using LBSJourney.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LBSJourney.Controllers
{
    public class UsersController : Controller
    {
        private ConnectionFactory _factory;
        private UsersDao _usersDao;
        private readonly ILogger _logger;
        private LbsDB _lbsDB;
        private AppSecurity _appSecurity;
        private AxiosBaseURL _axiosBaseURL;

        public UsersController(ConnectionFactory factory, UsersDao usersDao, ILogger<UsersController> logger, LbsDB lbsDB, AppSecurity appSecurity, AxiosBaseURL axiosBaseURL)
        {
            _factory = factory;
            _usersDao = usersDao;
            _logger = logger;
            _lbsDB = lbsDB;
            _appSecurity = appSecurity;
            _axiosBaseURL = axiosBaseURL;
        }

        public IActionResult Index()
        {
            HttpContext context = this.HttpContext;
            ISession session = context.Session;
            String userName = session.GetString(".UserName");
            // Console.WriteLine($"{userName}");
            ViewData["UserName"] = userName;
            String hashUserName = _appSecurity.hash512(userName);
            var result = _lbsDB.Users.Find(hashUserName);
            if (result != null) 
            {
                String email = result.Email;
                ViewData["Email"] = email;
            }
            String baseURL = _axiosBaseURL.url;
            ViewData["BaseURL"] = baseURL;
            return View();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("/api/users")]
        public IActionResult Insert([FromBody]Users json)
        {
            Message msg = new Message();
            String? message = null;
            if (this.Request.Method.Equals("POST"))
            {
                if (this._usersDao.Insert(json))
                {
                    message = $"使用者: {json.UserName} 新增成功";
                    msg.code = 200;
                    msg.msg = message;
                }
                else
                {
                    message = $"使用者: {json.UserName} 新增失敗";
                    msg.code = 400;
                    msg.msg = message;
                    return BadRequest(msg);
                }
            }
            return Ok(msg);
        }
    }
}
