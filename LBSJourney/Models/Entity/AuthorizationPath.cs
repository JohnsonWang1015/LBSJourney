namespace LBSJourney.Models.Entity
{
    public class AuthorizationPath
    {
        public string denyPath { get; set; }
        public string allowPath { get; set; }
        public string salt { get; set; }
    }
}
