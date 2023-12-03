namespace LBSJourneyWebService.Models.Dropbox
{
    public class ListFolderRequest
    {
        public bool include_deleted { get; set; }
        public bool include_has_explicit_shared_members { get; set; }
        public bool include_media_info { get; set; }
        public bool include_mounted_folders { get; set; }
        public bool include_non_downloadable_files { get; set; }
        public string path { get; set; }
        public bool recursive { get; set; }
        public Shared_Link shared_link { get; set; }
    }

    public class Shared_Link
    {
        public string url { get; set; }
        public string password { get; set; }
    }

}
