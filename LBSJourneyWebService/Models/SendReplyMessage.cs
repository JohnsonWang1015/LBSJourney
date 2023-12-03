namespace LBSJourneyWebService.Models
{

    public class SendReplyMessage
    {
        public string replyToken { get; set; }
        public ReplyMessage[] messages { get; set; }
    }

    public class ReplyMessage
    {
        public string type { get; set; }
        public string text { get; set; }
    }

}
