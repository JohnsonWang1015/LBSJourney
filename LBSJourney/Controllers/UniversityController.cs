using LBSJourney.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LBSJourney.Controllers
{
    public class UniversityController : Controller
    {
        private IHttpClientFactory _clientFactory;
        private ServiceURL _url;

        public UniversityController(IHttpClientFactory clientFactory, ServiceURL url)
        {
            _clientFactory = clientFactory;
            _url = url;
        }

        public IActionResult Index()
        {
            HttpClient client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(this._url.universityService);
            List<University>? result = client.GetFromJsonAsync<List<University>>("").GetAwaiter().GetResult();
            Console.WriteLine(result);
            return View(result);
        }
    }
}
