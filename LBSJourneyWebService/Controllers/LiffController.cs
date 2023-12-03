using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LBSJourneyWebService.Controllers;

// [EnableCors("AllClient")]
[Route("api/v1/[controller]")]
[ApiController]
public class LiffController:ControllerBase
{
    /// <summary>
    /// LINE LIFF ID 取得
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Route("getId/{name}")]
    [HttpGet]
    [Produces("text/plain")]
    public IActionResult GetLIFFID([FromRoute(Name = "name")]String name)
    {
        if (name.Equals("webApp"))
        {
            return Ok("2001256902-Kz2DAkne");
        }

        return Ok("");
    }
}