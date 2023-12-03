using LBSJourneyWebService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LBSJourneyWebService.Controllers
{
    /// <summary>
    /// LINE WebHook 控制器
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LineWebHookController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private LineServiceAPI lineServiceAPI;

        public LineWebHookController(IHttpClientFactory httpClientFactory, LineServiceAPI lineServiceAPI)
        {
            _httpClientFactory = httpClientFactory;
            this.lineServiceAPI = lineServiceAPI;
        }

        /// <summary>
        /// WebHook API 接口
        /// </summary>
        /// <param name="webHookData"></param>
        /// <returns></returns>
        [Route("callback")]
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult LineWebHook([FromBody]WebHookData webHookData)
        {
            try
            {
                String userId = webHookData.events[0].source.userId;
                if (webHookData.events[0].type.Equals("message"))
                {
                    String replyToken = webHookData.events[0].replyToken;
                    String replyMessageURL = lineServiceAPI.replyMessageUrl;
                    if (webHookData.events[0].message.type.Equals("text"))
                    {
                        String userMessage = webHookData.events[0].message.text;
                        String token = lineServiceAPI.token;
                        SendReplyMessage sendReplyMessage = new SendReplyMessage()
                        {
                            replyToken=replyToken,
                            messages=new ReplyMessage[]
                            {
                                new ReplyMessage()
                                {
                                    type="text",
                                    text=$"你說: {userMessage}"
                                }
                            }
                        };
                        String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendReplyMessage);
                        HttpClient httpClient = _httpClientFactory.CreateClient();
                        httpClient.BaseAddress = new Uri(replyMessageURL);
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        HttpContent httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        httpClient.PostAsync("", httpContent).GetAwaiter().GetResult();
                        return Ok(userMessage);
                    }
                    else if (webHookData.events[0].message.type.Equals("location"))
                    {
                        float latitude = (float)webHookData.events[0].message.latitude;
                        float longitude = (float)webHookData.events[0].message.longitude;
                        String address = webHookData.events[0].message.address;
                        // Console.WriteLine($"{latitude}\n{longitude}");
                        // Console.WriteLine($"{address}");
                        SendReplyMessage sendReplyMessage = new SendReplyMessage()
                        {
                            replyToken = replyToken,
                            messages = new ReplyMessage[]
                            {
                                new ReplyMessage()
                                {
                                    type="text",
                                    text=$"你的位置: {address}"
                                }
                            }
                        };
                        String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendReplyMessage);
                        HttpClient httpClient = _httpClientFactory.CreateClient();
                        httpClient.BaseAddress = new Uri(replyMessageURL);
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", lineServiceAPI.token);
                        HttpContent httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                        httpClient.PostAsync("", httpContent).GetAwaiter().GetResult();
                        return Ok("收到位置資訊");
                    }
                    else
                    {
                        return BadRequest("不是文字訊息");
                    }
                }
                else
                {
                    return BadRequest("不是聊天訊息內容");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
