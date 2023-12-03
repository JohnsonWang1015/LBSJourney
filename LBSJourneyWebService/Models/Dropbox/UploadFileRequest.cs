namespace LBSJourneyWebService.Models.Dropbox
{
    
    public class UploadFileRequest
    {
        public bool autorename { get; set; }
        public string mode { get; set; }
        public bool mute { get; set; }
        public string path { get; set; }
        public bool strict_conflict { get; set; }
    }

}
