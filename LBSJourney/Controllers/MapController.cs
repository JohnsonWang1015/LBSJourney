using LBSJourney.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LBSJourney.Controllers;

public class MapController:Controller
{
    private AxiosBaseURL _axiosBaseURL;
    public MapController(AxiosBaseURL axiosBaseURL)
    {
        _axiosBaseURL = axiosBaseURL;
    }

    public IActionResult Index()
    {
        HttpContext context = this.HttpContext;
        ISession session = context.Session;
        String userName = session.GetString(".UserName");
        String baseURL = _axiosBaseURL.url;
        ViewData["UserName"] = userName;
        ViewData["BaseURL"] = baseURL;
        return View();
    }

    [Route("[controller]/home")]
    public IActionResult AllUsersMap()
    {
        String baseURL = _axiosBaseURL.url;
        ViewData["BaseURL"] = baseURL;
        return View();
    }
}