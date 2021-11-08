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
    public class CuentaController : ControllerBase
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public CuentaController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllCuentas")]
        public IActionResult GetAllCuentas()
        {
            var data = DbClientFactory<CuentaDbClient>.Instance.GetAllCuentas(appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpPost]
        [Route("AddCuenta")]
        public IActionResult AddCuenta([FromBody] CuentaAhorroModel model)
        {
            var msg = new Message<CuentaAhorroModel>();
            var data = DbClientFactory<CuentaDbClient>.Instance.AddCuenta(model, appSettings.Value.DbConn);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (model.Id == 0)
                    msg.ReturnMessage = "Cuenta guardada con exito";
                else
                    msg.ReturnMessage = "cliente actualizado con exito";
            }
            else if (data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "No existe cliente";
            }
            return Ok(msg);
        }
    }
}
