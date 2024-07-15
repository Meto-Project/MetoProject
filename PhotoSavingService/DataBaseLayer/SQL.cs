using PhotoSavingService.Controllers;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PhotoSavingService.Models;



namespace PhotoSavingService.DataBaseLayer 
{
    public class SQL 
    {
        public readonly IConfiguration _configuration;
        public string _connectionString;
        public string _sp_DATAKAYDET;

        public SQL(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _sp_DATAKAYDET = _configuration["StoredProcedures:sp_DATAKAYDET"];
        }
        public async void SpExecute(RegistrationModel registrationModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(_sp_DATAKAYDET, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PhotoName", registrationModel.PhotoName);
                    cmd.Parameters.AddWithValue("@RegistrationTime", registrationModel.RegistrationTime);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
            catch (Exception e){}
        }
    }
}
