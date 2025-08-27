using LibraryManagementSystem.IRepository;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem.Repository
{

    public class AccountManagement:IAccountManagement
    {
        private readonly string? _connectionString;
        public AccountManagement(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<CompanyDetails> AuthenticateUserAsync(string username, string password)
        {
            DataTable dt = new DataTable();
            CompanyDetails compdet = new CompanyDetails();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Sp_userlogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@action", "login");
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    SqlDataReader read = await cmd.ExecuteReaderAsync();
                    if (read != null)
                    {
                        if (await read.ReadAsync())
                        {
                            compdet.UId = int.Parse(read["uid"]?.ToString() ?? "0");
                            compdet.UserName = read["uname"]?.ToString() ?? "0";
                            compdet.Companame = read["CompanyName"]?.ToString() ?? "0";
                            compdet.IsActive = bool.Parse(read["isactive"]?.ToString() ?? "0");
                            compdet.trialst = bool.Parse(read["trialst"]?.ToString() ?? "0");
                            compdet.Userrole = read["userrole"]?.ToString() ?? "0";
                            compdet.dbname = read["dbname"]?.ToString() ?? "0";
                            compdet.password = read["upass"]?.ToString() ?? "0";
                            compdet.Compid = int.Parse(read["compid"]?.ToString() ?? "0");
                            compdet.StartDate = DateTime.Parse(read["startdate"]?.ToString() ?? "0");
                            compdet.EndDate = DateTime.Parse(read["enddate"]?.ToString() ?? "0");

                        }
                    }
                }
                return compdet;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
