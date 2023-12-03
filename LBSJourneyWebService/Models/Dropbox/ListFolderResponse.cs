namespace LBSJourneyWebService.Models.Dropbox
{
    
    public class ListFolderResponse
    {
        public Entry[] entries { get; set; }
        public string cursor { get; set; }
        public bool has_more { get; set; }
    }

    public class Entry
    {
        public string tag { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public DateTime client_modified { get; set; }
        public DateTime server_modified { get; set; }
        public string rev { get; set; }
        public int size { get; set; }
        public bool is_downloadable { get; set; }
        public string content_hash { get; set; }
    }

}
