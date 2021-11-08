using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiAdoDemo.Model;
using CoreApiAdoDemo.Repository;
using CoreApiAdoDemo.Utility;
using Microsoft.Extensions.Options;

namespace CoreApiAdoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public TransaccionController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllTransacciones")]
        public IActionResult GetAllTransacciones()
        {
            var data = DbClientFactory<TransaccionDbClient>.Instance.GetAllTransacciones(appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpPost]
        [Route("AddTransaccion")]
        public IActionResult AddTransaccion([FromBody] TransaccionModel model)
        {
            var msg = new Message<TransaccionModel>();
            var data = DbClientFactory<TransaccionDbClient>.Instance.AddTransaccion(model, appSettings.Value.DbConn);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (model.Id == 0)
                    msg.ReturnMessage = "Transaccion guardada con exito";
                else
                    msg.ReturnMessage = "cliente actualizado con exito";
            }
            else if (data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "No existe cuenta de ahorro";
            }
            return Ok(msg);
        }
    }
}
