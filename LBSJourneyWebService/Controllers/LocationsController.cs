using System;
using LBSJourneyWebService.Models;
using LBSJourneyWebService.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBSJourneyWebService.Controllers
{
    /// <summary>
    /// 定位資料 API
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private LbsDb _lbsDb;
        private Salts _salts;

        public LocationsController(LbsDb lbsDb, Salts salts)
        {
            _lbsDb = lbsDb;
            _salts = salts;
        }

        /// <summary>
        /// 查詢所有使用者定位資料
        /// </summary>
        /// <returns></returns>
        [Route("all/users")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Locations>), 200)]
        public IActionResult getAllUsersPosition()
        {
            var result = (from p in _lbsDb.Locations
                          select p).ToList();
            Console.WriteLine(result);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("查無定位資料");
            }
        }

        /// <summary>
        /// 紀錄經緯度資料
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        [Route("record")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(StatusMessage), 200)]
        public IActionResult recordPosition([FromBody]Locations locations)
        {
            StatusMessage msg = new StatusMessage();
            locations.LocationId = Guid.NewGuid();
            locations.CreateTime = DateTime.Now;
            String hashUserName = HashUtility.hashData(locations.UserName, _salts.secretKeySalt);
            locations.UserName = hashUserName;
            // Console.WriteLine(hashUserName);
            // Console.WriteLine($"{locations.Latitude}");
            try
            {
                _lbsDb.Locations.Add(locations);
                _lbsDb.SaveChanges();
                msg.code = 200;
                msg.msg = $"定位資料新增成功";
                return Ok(msg);
            }
            catch(DbUpdateException ex)
            {                 
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
