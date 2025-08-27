namespace LibraryManagementSystem.Models
{
    public class userDetails
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        List<CompanyDetails>? CompanyDetails { get; set; }

    }
    public class CompanyDetails
    {
        public int Compid { get; set; }
        public string Companame { get; set; } = string.Empty;
        public string Userrole { get; set; } = string.Empty;
        public string Fyear { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public string UserName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int UId { get; set; }
        public bool IsActive { get; set; }
        public bool trialst { get; set; }
        public string dbname { get; set; } = string.Empty;
    }
}
