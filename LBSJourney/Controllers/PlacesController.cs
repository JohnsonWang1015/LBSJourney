using Antelcat.Attributes;
using LBSJourney.Models;
using LBSJourney.Models.Dao;
using LBSJourney.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LBSJourney.Controllers
{
    public class PlacesController : Controller
    {
        [Autowired]
        private PlacesDao placesDao;
        private AxiosBaseURL _axiosBaseURL;
        private LbsDB _lbsDB;

        public PlacesController(AxiosBaseURL axiosBaseURL, LbsDB lbsDB)
        {
            _axiosBaseURL = axiosBaseURL;
            _lbsDB = lbsDB;
        }

        public IActionResult Index()
        {
            String pythonBaseURL = _axiosBaseURL.pythonUrl;
            ViewData["pythonBaseURL"] = pythonBaseURL;
            return View();
        }

        [Route("[controller]/list")]
        public IActionResult PlacesList()
        {
            var placeList = (from p in _lbsDB.Places
                             select p).ToList();
            if (placeList.Count > 0)
            {
                ViewData["placeList"] = Newtonsoft.Json.JsonConvert.SerializeObject(placeList);
            }
            return View();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("/api/place")]
        public IActionResult Insert([FromBody]Places json)
        {
            return Json("");
        }

        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("/api/place")]
        public IActionResult Update([FromBody]Places json)
        {
            return Json("");
        }

        [HttpDelete]
        [Produces("application/json")]
        [Route("/api/place/{id}")]
        public IActionResult Delete([FromRoute(Name = "id")]String placeId)
        {
            return Json("");
        }
        
    }
}
