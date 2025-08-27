namespace LibraryManagementSystem.DynamicConnection
{
    public static class Connection
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //public static string dbcs = 
        public static string ConnectionString { 
            get 
            {
              return $"Server=DESKTOP-BMNHJFM\\SQLEXPRESS;Database={_httpContextAccessor?.HttpContext?.Session.GetString("dbname")?.ToString() ?? ""};Trusted_Connection=True;Connection Timeout=30;TrustServerCertificate=True";
            } 
        }
    }
}
