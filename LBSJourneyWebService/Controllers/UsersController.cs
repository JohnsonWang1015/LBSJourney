using LBSJourneyWebService.Models;
using LBSJourneyWebService.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBSJourneyWebService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController:ControllerBase
{
    private LbsDb lbsDb;
    private Salts salts;

    public UsersController(LbsDb lbsDb, Salts salts)
    {
        this.lbsDb = lbsDb;
        this.salts = salts;
    }

    /// <summary>
    /// 成員註冊
    /// </summary>
    /// <param name="users">使用者資料</param>
    /// <returns></returns>
    [Route("register/rawdata")]
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    public IActionResult register([FromBody]Users users)
    {
        String hashUserName = HashUtility.hashData(users.UserName, salts.secretKeySalt);
        String hashPassword = HashUtility.hashData(users.Password, salts.secretKeySalt);
        users.UserName = hashUserName;
        users.Password = hashPassword;

        // try
        // {
        //     lbsDb.Users.Add(users);
        //     
        // }
        // catch (DbUpdateException ex)
        // {
        //     
        // }
        
        return new ObjectResult(hashUserName);
    }

    /// <summary>
    /// 電子郵件修改
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    [Route("modify/email")]
    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(StatusMessage), 200)]
    [ProducesResponseType(typeof(StatusMessage), 400)]
    public IActionResult emailModify([FromBody]Users users)
    {
        StatusMessage msg = new StatusMessage();
        String email = users.Email;
        String hashUserName = HashUtility.hashData(users.UserName, salts.secretKeySalt);
        var result = (from u in lbsDb.Users
                        where u.UserName == hashUserName
                        select u).FirstOrDefault();
        if(result != null)
        {
            result.Email = email;
            lbsDb.Users.Update(result);
            lbsDb.SaveChanges();
            msg.code = 200;
            msg.msg = $"電子郵件修改成功";
            return Ok(msg);
        }
        else
        {
            msg.code = 400;
            msg.msg = $"電子郵件修改失敗";
            return BadRequest(msg);
        }
        
    }
}