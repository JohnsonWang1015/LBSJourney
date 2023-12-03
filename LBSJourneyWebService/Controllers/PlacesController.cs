using LBSJourneyWebService.Models;
using LBSJourneyWebService.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LBSJourneyWebService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PlacesController:ControllerBase
{
    private LbsDb lbsDb;

    public PlacesController(LbsDb lbsDb)
    {
        this.lbsDb = lbsDb;
    }

    /// <summary>
    /// 景點新增
    /// </summary>
    /// <param name="places">景點資料</param>
    /// <returns></returns>
    [Route("insert/rawdata")]
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(StatusMessage), 200)]
    [ProducesResponseType(typeof(StatusMessage), 400)]
    public IActionResult insertPlaces(Places places)
    {
        return new ObjectResult(places);
    }
}