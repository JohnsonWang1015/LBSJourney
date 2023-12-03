namespace LBSJourneyWebService.Models
{

    public class WebHookData
    {
        public string destination { get; set; }
        public Event[] events { get; set; }
    }

    public class Event
    {
        public string type { get; set; }
        public Message message { get; set; }
        public long timestamp { get; set; }
        public Source source { get; set; }
        public string replyToken { get; set; }
        public string mode { get; set; }
        public string webhookEventId { get; set; }
        public Deliverycontext deliveryContext { get; set; }
    }

    public class Message
    {
        public string? type { get; set; }
        public string? id { get; set; }
        public string? text { get; set; }
        public string? title { get; set; }
        public string? address { get; set; }
        public float? latitude { get; set; }
        public float? longitude { get; set; }
    }

    public class Source
    {
        public string type { get; set; }
        public string userId { get; set; }
    }

    public class Deliverycontext
    {
        public bool isRedelivery { get; set; }
    }

}
