namespace LBSJourneyWebService.Models.Dropbox
{
    public class CreateFolderResponse
    {
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public string name { get; set; }
        public string path_lower { get; set; }
        public string path_display { get; set; }
        public string id { get; set; }
    }
}
