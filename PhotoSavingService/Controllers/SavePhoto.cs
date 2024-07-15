

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PhotoSavingService.DataBaseLayer;
using PhotoSavingService.Models;

namespace PhotoSavingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavePhoto : ControllerBase
    {
        private readonly SQL sql;

        [HttpPost]
        public async Task<IActionResult> PostPhoto([FromBody] RegistrationModel registrationModel)
        {
            if (registrationModel == null)
            {
                return BadRequest();
            }
            sql.SpExecute(registrationModel);
            return Ok();
        }
    }
}
