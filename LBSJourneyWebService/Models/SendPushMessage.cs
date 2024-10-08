﻿namespace LBSJourneyWebService.Models
{

    public class SendPushMessage
    {
        public string to { get; set; }
        public PushMessage[] messages { get; set; }
    }

    public class PushMessage
    {
        public string type { get; set; }
        public string text { get; set; }
    }

}
