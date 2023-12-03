using LBSJourney.Models.Entity;
using Microsoft.Data.SqlClient;

namespace LBSJourney.Models.Dao
{
    public class UsersDao : IDaoOperations<Users, String>
    {
        private ConnectionFactory _factory;
        public ConnectionFactory Factory 
        {
            get
            {
                return _factory;
            }
            set
            {
                _factory = value;
            }
        }

        public bool Delete(string id)
        {
            return false;
        }

        public List<Users> FindAll()
        {
            if(_factory == null)
            {
                throw new Exception("連接工廠物件不可為 null");
            }
            using (SqlConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "Select UserId,UserName,Email,Password From Users;";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Users> list = new List<Users>();
                    while(reader.Read())
                    {
                        Users users = new Users()
                        {
                            //UserId = reader["UserId"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                        list.Add(users);
                    }
                    return list;
                }catch(SqlException ex)
                {
                    return null;
                }
            }
        }

        public Users FindById(string id)
        {
            Users users = null;
            if (_factory == null)
            {
                throw new Exception("連接工廠物件不可為 null");
            }
            using (SqlConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "Select UserId,UserName,Email,Password From Users Where UserId=@UserId;";
                    cmd.Parameters.AddWithValue("UserId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        users = new Users()
                        {
                            //UserId = reader["UserId"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                    }
                }
                catch(SqlException ex)
                {
                    return null;
                }
            }
            return users;
        }

        public bool Insert(Users source)
        {
            if(_factory == null)
            {
                throw new Exception("連接工廠物件不可為 null");
            }
            using (SqlConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "Insert Into Users(UserName,Email,Password) values(@UserName,@Email,@Password);";
                    cmd.Parameters.AddWithValue("UserName", source.UserName);
                    cmd.Parameters.AddWithValue("Email", source.Email);
                    cmd.Parameters.AddWithValue("Password", source.Password);
                    int affectRows = cmd.ExecuteNonQuery();
                    if(affectRows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }

        public bool Update(Users source)
        {
            if (_factory == null)
            {
                throw new Exception("連接工廠物件不可為 null");
            }
            using (SqlConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "Update Users Set UserName=@UserName, Email=@Email, Password=@Password Where UserId=@UserId;";
                    cmd.Parameters.AddWithValue("UserName", source.UserName);
                    cmd.Parameters.AddWithValue("Email", source.Email);
                    cmd.Parameters.AddWithValue("Password", source.Password);
                    //cmd.Parameters.AddWithValue("UserId", source.UserId);
                    int affectRows = cmd.ExecuteNonQuery();
                    if(affectRows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }
    }
}
