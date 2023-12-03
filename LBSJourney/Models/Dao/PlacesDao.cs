using LBSJourney.Models.Entity;
using Microsoft.Data.SqlClient;

namespace LBSJourney.Models.Dao
{
    public class PlacesDao : IDaoOperations<Places, String>
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

        public List<Places> FindAll()
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
                    cmd.CommandText = "Select PlaceId,Name,Address,Description,PictureUrl,Score,Latitude,Longitude From Places;";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Places> result = new List<Places>();
                    while (reader.Read())
                    {
                        Places places = new Places()
                        {
                            PlaceId = (Guid?)reader["PlaceId"],
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Description = reader["Description"].ToString(),
                            PictureUrl = reader["PictureUrl"].ToString(),
                            Score = Convert.ToInt32(reader["Score"]),
                            Latitude = (String?)(reader["Latitude"]),
                            Longitude = (String?)(reader["Longitude"])
                        };
                        result.Add(places);
                    }
                    return result;
                }catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public Places FindById(string id)
        {
            return null;
        }

        public bool Insert(Places source)
        {
            return false;
        }

        public bool Update(Places source)
        {
            return false;
        }
    }
}
