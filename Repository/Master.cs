using LibraryManagementSystem.DynamicConnection;
using LibraryManagementSystem.IRepository;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem.Repository
{
    public class Master: IMaster
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
		public readonly int _uid;
		public readonly int _compid;
        public Master(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_connectionString = Connection.ConnectionString;
             _uid = int.Parse(_httpContextAccessor.HttpContext?.Session.GetString("uid") ?? "0");
             _compid = int.Parse(_httpContextAccessor.HttpContext?.Session.GetString("compid") ?? "0");
        }
        public async Task<string> saveHour(Hour hour)
        {
			string msg =string.Empty;   
            try
			{
				using (SqlConnection con = new SqlConnection(_connectionString))
				{
					if (con.State == ConnectionState.Closed)
						await con.OpenAsync();
					using (SqlCommand cmd = new SqlCommand("Sp_Student", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action", "SaveHour");
                        cmd.Parameters.AddWithValue("@id", hour.id);
						cmd.Parameters.AddWithValue("@Name", hour.hournumber);
                        cmd.Parameters.AddWithValue("@uid", _uid);
                        cmd.Parameters.AddWithValue("@compid", _compid);
                        cmd.Parameters.AddWithValue("@IsActive", hour.action);
						using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
						{
							while (await dr.ReadAsync())
							{
								msg = dr["msg"].ToString() ?? "500:Message Not Found";
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}
			return msg;
        }
        public async Task<List<Hour>> GetHourdetails()
        {
            List<Hour> hour_Models = new List<Hour>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "GetHour");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader da = await cmd.ExecuteReaderAsync();
                    while (await da.ReadAsync())
                    {
                        var newdata = new Hour()
                        {
                            id = da.GetInt32(0),
                            hournumber = da.GetString(1),
                            action = da.GetString(3),
                            curdate= da.GetString(2)
                        };
                        hour_Models.Add(newdata);
                    }
                    if (da != null)
                    {
                        await da.CloseAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return hour_Models;
        }
        public async Task<string> DeleteHour(int id)
        {
            string msg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@action", "Deletehour");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader read = await cmd.ExecuteReaderAsync();
                    if (await read.ReadAsync())
                    {
                        msg = read["msg"].ToString()??"";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }
        public async Task<string> saveCourse(course cm)
        {
            string msg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", cm.Id);
                    cmd.Parameters.AddWithValue("@uid", _uid);
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    cmd.Parameters.AddWithValue("@Name", cm.Course);
                    cmd.Parameters.AddWithValue("@IsActive", cm.action);
                    cmd.Parameters.AddWithValue("@action", "SaveCourse");

                    SqlDataReader read = await cmd.ExecuteReaderAsync();
                    if (await read.ReadAsync())
                    {
                        msg = read["msg"].ToString()??"";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }
        public async Task<List<course>> getCourseDetails()
        {
            List<course> courseList = new List<course>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "getCourse");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        var newdata = new course()
                        {
                            Id = dr.GetInt32(0),
                            Course = dr.GetString(1),
                            action = dr.GetString(3),
                            curdate = dr.GetString(2)
                        };
                        courseList.Add(newdata);
                    }
                    if (dr != null)
                    {
                        await dr.CloseAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return courseList;
        }
        public async Task<string> DeleteCourse(int id)
        {
            string msg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@action", "DeleteCourse");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader read = await cmd.ExecuteReaderAsync();
                    if (await read.ReadAsync())
                    {
                        msg = read["msg"].ToString()??"";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }
        public async Task<string> saveMembership(Membership cm)
        {
            string msg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_Student", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", cm.id);
                        cmd.Parameters.AddWithValue("@uid", _uid);
                        cmd.Parameters.AddWithValue("@compid", _compid);
                        cmd.Parameters.AddWithValue("@Name", cm.membership);
                        cmd.Parameters.AddWithValue("@desc", cm.description);
                        cmd.Parameters.AddWithValue("@IsActive", cm.Action);
                        cmd.Parameters.AddWithValue("@action", "SaveMembership");

                        using (SqlDataReader read = await cmd.ExecuteReaderAsync())
                        {
                            while (await read.ReadAsync())
                            {
                                msg = read["msg"].ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }
        public async Task<List<Membership>> getMembershipDetails()
        {
            List<Membership> Memberships = new List<Membership>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "getmembership");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        var newdata = new Membership()
                        {
                            id = dr.GetInt32(0),
                            membership = dr.GetString(1),
                            description = dr.GetString(2),
                            curdate = dr.GetString(3),
                            Action = dr.GetString(4)
                        };
                        Memberships.Add(newdata);
                    }
                    if (dr != null)
                    {
                        await dr.CloseAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Memberships;
        }
        public async Task<string> DeleteMembership(int id)
        {
            string msg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@action", "DeleteMembership");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader read = await cmd.ExecuteReaderAsync();
                    if (await read.ReadAsync())
                    {
                        msg = read["msg"].ToString() ?? "";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }
        public async Task<List<CardType>> getCardTypesDetails()
        {
            List<CardType> CardType = new List<CardType>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("Sp_Student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "getCardType");
                    cmd.Parameters.AddWithValue("@compid", _compid);
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        var newdata = new CardType()
                        {
                            id = dr.GetInt32(0),
                            cardname = dr.GetString(1),
                            CardDesc = dr.GetString(2),
                            basePrice = decimal.Parse(dr.GetString(3)),
                            curdate = dr.GetString(4),
                            Action = dr.GetString(5)
                        };
                        CardType.Add(newdata);
                    }
                    if (dr != null)
                    {
                        await dr.CloseAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CardType;
        }
    }
}
