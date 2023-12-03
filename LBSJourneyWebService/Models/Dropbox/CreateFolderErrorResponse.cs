namespace LBSJourneyWebService.Models.Dropbox
{
    public class CreateFolderErrorResponse
    {
        public string error_summary { get; set; }
        public Error error { get; set; }
    }

    public class Error
    {
        public string tag { get; set; }
        public Path path { get; set; }
    }

    public class Path
    {
        public string tag { get; set; }
        public Conflict conflict { get; set; }
    }

    public class Conflict
    {
        public string tag { get; set; }
    }
}
