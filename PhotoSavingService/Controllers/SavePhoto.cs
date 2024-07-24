

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PhotoSavingService.DataBaseLayer;
using PhotoSavingService.Models;
using System.Net.Sockets;
using System.Text;

namespace PhotoSavingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavePhoto : ControllerBase
    {
        private readonly SQL sql;
        public SavePhoto(IConfiguration configuration)
        {
            sql = new SQL(configuration);
        }

        [HttpPost]
        public async Task<IActionResult> PostPhoto([FromBody] RegistrationModel registrationModel)
        {
            if (registrationModel == null)
            {
                return BadRequest();
            }
            sql.SpExecute(registrationModel);
            SendIPOverSocket("192.168.1.1");
            return Ok();
        }
        private void SendIPOverSocket(string ipAddress)
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 8888)) // Socket sunucusunun IP'si ve portu
            {
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(ipAddress);
                stream.Write(data, 0, data.Length);
            }
        }

    }
}
