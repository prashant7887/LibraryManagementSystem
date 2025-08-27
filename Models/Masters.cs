namespace LibraryManagementSystem.Models
{
    public class Hour
    {
        public int id { get; set; }
        public string? hournumber { get; set; }
        public string? action { get; set; }
        public string? curdate { get; set; }
    }
    public class course
    {
        public int Id { get; set; }
        public string? Course { get; set; }
        public string? action { get; set; }
        public string? curdate { get; set; }

    }
    public class Membership
    {
        public int id { get; set; }
        public string? membership { get; set; }
        public string? description { get; set; }
        public string? Action { get; set; }
        public string? curdate { get; set; }

    }
}
