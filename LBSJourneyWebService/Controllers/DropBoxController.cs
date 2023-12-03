using LBSJourneyWebService.Models.Dropbox;
using LBSJourneyWebService.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace LBSJourneyWebService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DropBoxController : ControllerBase
    {

        private Dropbox dropbox;
        private IHttpClientFactory _httpClientFactory;

        public DropBoxController(Dropbox dropbox, IHttpClientFactory httpClientFactory)
        {
            this.dropbox = dropbox;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 建立 DropBox 資料夾
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("createFolder")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateFolderResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult CreateFolder([FromBody]CreateFolderRequest request)
        {
            String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(dropbox.createFolder);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dropbox.token);
            HttpContent httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage message = httpClient.PostAsync("", httpContent).GetAwaiter().GetResult();
            if(message.IsSuccessStatusCode)
            {
                String result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                CreateFolderResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateFolderResponse>(result);
                return Ok(response);
            }
            else
            {
                String result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                CreateFolderErrorResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateFolderErrorResponse>(result);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// 上傳檔案到 DropBox
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("uploadFile")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var nonChineseFileName = Regex.Replace(file.FileName, "[\u4e00-\u9fa5]", string.Empty);
                var currentDate = DateTime.Now.ToString("yyyyMMdd");
                var path = $"/LBS/{currentDate}{nonChineseFileName}";

                UploadFileRequest request = new UploadFileRequest();
                request.autorename = false;
                request.mode = "add";
                request.mute = false;
                request.path = path;
                request.strict_conflict = false;

                String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri(dropbox.uploadFile);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dropbox.token);
                httpClient.DefaultRequestHeaders.Add("Dropbox-API-Arg", jsonString);

                var content = new StreamContent(file.OpenReadStream());
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                var response = await httpClient.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok(result);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 取得 DropBox 資料夾內容
        /// </summary>
        /// <returns></returns>
        [Route("listFolder")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ListFolderResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult ListFolder([FromBody]ListFolderRequest request)
        {
            String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(dropbox.listFolder);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", dropbox.basicToken);
            HttpContent httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage message = httpClient.PostAsync("", httpContent).GetAwaiter().GetResult();
            if (message.IsSuccessStatusCode)
            {
                String result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                ListFolderResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<ListFolderResponse>(result);
                return Ok(response);
            }
            else
            {
                String result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return BadRequest(result);
            }
        }

        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <returns></returns>
        [Route("downloadFile")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DownloadFileResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult DownloadFile([FromBody]DownloadFileRequest request)
        {
            String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(dropbox.downloadFile);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dropbox.token);
            HttpContent httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage message = httpClient.PostAsync("", httpContent).GetAwaiter().GetResult();
            if (message.IsSuccessStatusCode)
            {
                String result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                DownloadFileResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<DownloadFileResponse>(result);
                return Ok(response);
            }
            else
            {
                String result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return BadRequest(result);
            }
        }

    }
}
